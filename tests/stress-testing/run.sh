docker pull grafana/k6;

docker volume create k6-vol;

docker run  --cpus=1.5 --memory=4880m --net=infra_samplecrudnetwork --name k6-stress-test-container -v k6-vol:/data \
            --rm -i grafana/k6 run - <stress-script.js;