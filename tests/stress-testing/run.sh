docker pull grafana/k6;

# docker pull grafana/k6:master-with-browser;

docker run --net=host --rm -i grafana/k6 run - <stress-script.js;