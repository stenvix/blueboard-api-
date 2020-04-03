# BlueBoard API

## How to run app?
To run app you need install docker and docker compose:

 * [Windows](https://hub.docker.com/editions/community/docker-ce-desktop-windows)
 * [Linux](https://docs.docker.com/install/linux/docker-ce/debian/)

 After installation go to 'compose' folder and run command:

 ```
 docker-compose up
 ```

 If you want to run docker compose as daemon, use command below
 ```
 docker-compose up -d
 ```


 ## How to develop app?

If you want to improve app, you need to instal docker and run command below:

```
docker-compose -f docker-compose.development.yml up
```

This command will up infrastructure for local development