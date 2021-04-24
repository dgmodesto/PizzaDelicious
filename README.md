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

![Alt text](https://github.com/dgmodesto/PizzaDelicious/blob/main/images-readme/backend.png?raw=true "Title")


**************************************************************************

# Estrutura Frontend (em construção)

![Alt text](https://github.com/dgmodesto/PizzaDelicious/blob/main/images-readme/frontend.png?raw=true "Title")

**************************************************************************

# Estrutura Metrics

![Alt text](https://github.com/dgmodesto/PizzaDelicious/blob/main/images-readme/metrics.png?raw=true "Title")

- metrics
	- Qa
		- grafana
			- config
			- dashboards
			- provisioning			
		- prometheus
			- data
			- prometheus.yml
	- grafana.qa.Dockerfile
	- prometheus.qa.Dockerfile
