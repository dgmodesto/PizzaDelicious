FROM prom/prometheus:v2.17.1 as ap-prometheus
COPY ./Qa/prometheus/prometheus.yml /etc/prometheus

# Volumes (Host/Container)
VOLUME ./Qa/prometheus/data/prometheus
EXPOSE 9090
