FROM grafana/grafana:7.3.1 as ap-grafana
COPY  ./Qa/grafana/config/grafana.ini /etc/grafana
COPY  ./Qa/grafana/provisioning /etc/grafana/provisioning
COPY  ./Qa/grafana/dashboards /var/lib/grafana/dashboards
EXPOSE 3000
