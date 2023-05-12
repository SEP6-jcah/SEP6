cd movieservice
docker build --no-cache --progress=plain -t movieservice .
docker create -p 8080:80 --name movieservice  movieservice
cd ..