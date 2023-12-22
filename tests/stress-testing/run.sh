docker pull grafana/k6;

docker run  --cpus=0.5 --memory=0.5 --net=infra_samplecrudnetwork \
            --rm -i grafana/k6 run - <local-import-person-data.js;

# docker run --net=host -v ./../mass/:/usr/share/mass/ --rm -i grafana/k6 run - <local-import-person-data.js
