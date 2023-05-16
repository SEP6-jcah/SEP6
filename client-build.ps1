cd .\Sep6Client
docker build --no-cache --progress=plain -t client .
docker create -p 8081:80 --net sep-net --name client client
cd ..