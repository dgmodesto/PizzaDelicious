# Pizza Delicious

Implementado por: <b>Douglas Gomes Modesto<b>

<hr>

## Sobre o projeto
  - Com a motivação de colocar meus conhecimento a prova, este projeto visa criar um ecommerce com foco em pizzarias. 

## Objetivo 
  - é criar um Portal para que os clientes possam realizar seus pedidos e visualizar os produtos da Pizzaria 

  - Criar um sistema de backoffice para que a Gerência da Pizzaria possa gerenciar seu estoque e promover possíveis promoções para seus clientes.


## Tecnologias Utilizadas
  - ### Backend
    - .Net Core 3.1
  - ### Frontend
    - Angular 11.0.0
  - ### metrics
    - Grafana
    - Prometheus
    - Docker
  
## Roadmap
 - O backend a princípio será implementado como um sistema Monolito, porém toda sua estrutura está sendo preparada para se trabalhar com microserviços caso seje necessário.

 - O frontend será um monolito e devido ao seu objetivo acredito que uma arquitetura voltada a micro frontends seria muito trabalhoso de lidar.

**************************************************************************

# Estrutura Backend (em construção)

- Core
	- Communication
		- Mediator
			- IMediatorHandler
			- MediatorHandler
	- Data
		- IRepository
		- IUnitOfWork
	- DomainObjects
		- DTO
			- ListProductOrder
		- DomainException
		- Entity
		- IAggregateRoot
		- Validations
	- Extensions
		- EnumerableExtensions
	- Messages
		- CommomMessages
			- DomainEvents
				- DomainEvent
			- IntegrationEvents
				- IntegrationEvent
				- OrderConfirmedEvent
				- OrderProcessingCanceledEvent
				- OrderStartedEvent
				- OrderStockRejectEvent
			- Notifications
				- DomainNotification
				- DomainNotificationHandler
		- Command
		- Event
		- Message
		
**************************************************************************
- Catalog
	- Application
		- AutoMapper
			- DomainToViewModelMappingProfile
			- ViewModelToDomainMappingProfile
		- Services
			- IProductAppService
			- ProductAppService
		- ViewModels
			- CategoryViewModel
			- ProductViewModel
	- Data
		- Mappings
			- CategoryMapping
			- ProductMapping
		- Migrations
		- Repository
			- ProductRepository
		- CatalogContext
	- Domain
		- Events
			- ProductEventHandler
			- ProductLowStockEvent
		- Interfaces
			- IProductRepository
		- Models
			- Category
			- Dimension
			- Product
		- Services
			- IStockService
			- StockService
			
**************************************************************************
- Sale
	- Application
		- Commands
			- OrderCommandHandler
			- AddOrderItemCommand
			- ApplyVoucherOrderCommand
			- CancelProcessingOrderCommand
			- CancelProcessingOrderResetStockCommand
			- FinishOrderCommand
			- RemoveOrderItemCommand
			- StartOrderCommand
			- UpdateOrderCommand
		- Events
			- OrderEventHandler
			- OrderDraftStartEvent
			- OrderFinishedEvent
			- OrderItemAddedEvent
			- OrderProductAddedEvent
			- OrderProductRemovedEvent
			- OrderProductUpdatedEvent
			- OrderVoucherAppliedEvent
		- Queries
			- ViewModels
				- OrderViewModel
				- ShopCarItemViewModel
				- ShopCarPaymentViewModel
				- ShopCarViewModel
			- IOrderQueries
			- OrderQueries
	- Data
		- Extensions
			- MeidatorExtension
		- Mappings
			- OrderMapping
			- OrderItemMapping
			- VoucherMapping
		- Migrations
		- Repository
			- OrderRepository
		- SaleContext
	- Domain
		- Interfaces
			- IOrderRepository
		- Models
			- Order
			- OrderItem
			- Voucher
			- Product
			- Category
		- Services
		
**************************************************************************
- Register
	- Application
		- AutoMapper
			- DomainToViewModelMappingProfile
			- ViewModelToDomainMappingProfile
		- Services
			- ClientService
			- IClientService
		- ViewModels
			- ClientViewModelaswxedcvfrloik,mjuyhn
	- Data
		- Mappings
			- AddressMapping
			- ClientMapping
		- Migrations
		- Repository
			- ClientRepository
		- RegisterContext		
	- Domain
		- Models
			- Client
			- Address
		- Interfaces
			- IRepository

**************************************************************************
- Payment

**************************************************************************
- Tax


**************************************************************************

# Estrutura Frontend (em construção)


**************************************************************************

# Estrutura Metrics
