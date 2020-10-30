FROM nshou/elasticsearch-kibana AS mpgpelastickibana

USER root

RUN apt-get install -y curl

USER elasticsearch

COPY ./docker/mpgpelastickibana/index.json /home/elasticsearch/

CMD (cd elasticsearch-7.9.0/bin/ && bash elasticsearch -E http.host=0.0.0.0 --quiet) \
    & (cd kibana-7.9.0-linux-x86_64/bin/ && bash kibana --host 0.0.0.0 -Q) \
    & sleep 60 \
    && curl -XPOST http://localhost:5601/api/saved_objects/index-pattern/13fd5de0-d09f-11e9-ac5e-6b76646c8bd0 -H 'Content-Type: application/json' -H 'kbn-xsrf: true' -d @index.json \
    && while true; do sleep 59 ; done

#curl -XGET http://localhost:5601/api/saved_objects/index-pattern/13fd5de0-d09f-11e9-ac5e-6b76646c8bd0 >> index.json