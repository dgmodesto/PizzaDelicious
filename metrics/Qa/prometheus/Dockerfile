FROM prom/prometheus:v2.17.1 as ap-prometheus
COPY ./prometheus.yml /etc/prometheus/

# Volumes (Host/Container)
VOLUME ./data/prometheus
EXPOSE 9090
