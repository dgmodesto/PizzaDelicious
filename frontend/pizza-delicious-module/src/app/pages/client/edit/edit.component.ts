import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { FormBuilder, FormControlName, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { utilsBr } from 'js-brasil';
import { NgBrazilValidators } from 'ng-brazil';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { FormBaseComponent } from 'src/app/base-components/form-base.components';
import { Address, CepSearch } from 'src/app/shared/models/address';
import { Client } from 'src/app/shared/models/client';
import { ClientService } from 'src/app/shared/services/client/client.service';
import { StringUtils } from 'src/app/shared/utils/string-utils';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent extends FormBaseComponent implements OnInit, AfterViewInit {

  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];


  errors: any[] = [];
  errorsAddress: any[] = [];

  clientForm: FormGroup;
  addressForm: FormGroup;


  client: Client = new Client();
  address: Address = new Address();

  textDocument = 'CPF (requerido)';
  formResult = '';
  MASKS = utilsBr.MASKS;

  constructor(
    private fb: FormBuilder,
    private clientService: ClientService,
    private router: Router,
    private route: ActivatedRoute,
    private modalService: NgbModal,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService
  ) {
    super();

    this.validationMessages = {
      name: {
        required: 'Informe o Nome',
      },
      document: {
        required: 'Informe o Documento',
        cpf: 'CPF em formato inválido',
      },
      publicPlace: {
        required: 'Informe o Logradouro',
      },
      number: {
        required: 'Informe o Número',
      },
      district: {
        required: 'Informe o Bairro',
      },
      cep: {
        required: 'Informe o CEP',
        cep: 'CEP em formato inválido'
      },
      city: {
        required: 'Informe a Cidade',
      },
      state: {
        required: 'Informe o Estado',
      }
    };

    super.configureMessagesValidationBase(this.validationMessages);
    this.client = this.route.snapshot.data.client;

  }

  ngOnInit(): void {
    this.spinner.show();

    this.clientForm = this.fb.group({
      name: ['', [Validators.required]],
      document: ['', [Validators.required, NgBrazilValidators.cpf]],
      enable: ['', [Validators.required]],
    });

    this.addressForm = this.fb.group({
      publicPlace: ['', [Validators.required]],
      number: ['', [Validators.required]],
      complement: [''],
      district: ['', [Validators.required]],
      cep: ['', [Validators.required, NgBrazilValidators.cep]],
      city: ['', [Validators.required]],
      state: ['', [Validators.required]]
    });

    this.fillForm();

    setTimeout(() => {
      this.spinner.hide();
    }, 1000);
  }

  fillForm(): void {

    this.addressForm.patchValue({
      id: this.client.address.id,
      publicPlace: this.client.address.publicPlace,
      number: this.client.address.number,
      complement: this.client.address.complement,
      district: this.client.address.district,
      cep: this.client.address.cep,
      city: this.client.address.city,
      state: this.client.address.state
    });


    this.clientForm.patchValue({
      id: this.client.id,
      name: this.client.name,
      enable: this.client.enable,
      document: this.client.document,
    });
  }

  ngAfterViewInit(): void {
    super.configureValidationFormBase(this.formInputElements, this.clientForm);
  }

  searchCep(cep: string): void {

    cep = StringUtils.justNumbers(cep);
    if (cep.length < 8) { return; }

    this.clientService.searchCep(cep)
      .subscribe(
        cepRetorno => this.fillAddressSearch(cepRetorno),
        erro => this.errors.push(erro));
  }

  fillAddressSearch(cepSearch: CepSearch): void {

    this.clientForm.patchValue({
      address: {
        publicPlace: cepSearch.logradouro,
        district: cepSearch.bairro,
        cep: cepSearch.cep,
        city: cepSearch.localidade,
        state: cepSearch.uf,
        complement: cepSearch.complemento,
        clientId: ''
      }
    });
  }


  updateClient(): void {
    if (this.clientForm.dirty && this.clientForm.valid) {

      this.client = Object.assign({}, this.client, this.clientForm.value);
      this.formResult = JSON.stringify(this.client);

      this.client.address.cep = StringUtils.justNumbers(this.client.address.cep);
      this.client.document = StringUtils.justNumbers(this.client.document);

      this.clientService.updateClient(this.client)
        .subscribe(
          sucesso => { this.processSuccess(sucesso); },
          falha => { this.processFail(falha); }
        );
    }
  }


  updateAddress(): void {
    if (this.addressForm.dirty && this.addressForm.valid) {

      this.address = Object.assign({}, this.address, this.addressForm.value);

      this.address.cep = StringUtils.justNumbers(this.address.cep);
      this.address.clientId = this.client.id;

      this.clientService.updateAddress(this.address)
        .subscribe(
          () => this.processSucessAddress(this.address),
          fail => { this.processFailAddress(fail); }
        );
    }
  }

  processSuccess(response: any): void {
    this.clientForm.reset();
    this.errors = [];

    this.changeNoSave = false;

    const toast = this.toastr.success('Fornecedor cadastrado com sucesso!', 'Sucesso!');
    if (toast) {
      toast.onHidden.subscribe(() => {
        this.router.navigate(['/fornecedores/listar-todos']);
      });
    }
  }

  processFail(fail: any): void {
    this.errors = fail.error.errors;
    this.toastr.error('Ocorreu um erro!', 'Opa :(');
  }

  processSucessAddress(address: Address): void {
    this.errors = [];

    this.toastr.success('Endereço atualizado com sucesso!', 'Sucesso!');
    this.client.address = address;
    this.modalService.dismissAll();
  }

  processFailAddress(fail: any): void {
    this.errorsAddress = fail.error.errors;
    this.toastr.error('Ocorreu um erro!', 'Opa :(');
  }

  openModel(content): void {
    this.modalService.open(content);
  }

}
