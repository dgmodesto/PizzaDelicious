

# Criar imagem do grafana no ambiente de QA
  - docker build -f ./grafana.qa.Dockerfile . -t grafana-qa

# Criar imagem do prometheus no ambiente de QA
  - docker build -f ./prometheus.qa.Dockerfile . -t prometheus-qa
