FROM grafana/grafana AS mpgpgrafana

ADD ./docker/mpgpgrafana/dashboards /var/lib/grafana/dashboards
ADD ./docker/mpgpgrafana/provisioning /etc/grafana/provisioning
ADD ./docker/mpgpgrafana/config.ini /etc/grafana/config.ini
