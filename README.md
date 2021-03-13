# Producer_Consumer_RabbitMQ_Demo
Producer service and Consumer service Demo (dotNet 5) with RabbitMQ - this demo uses all types of exchanges (direct, topic, header, fanout) - created for educational purposes

How to get it started.
First things first. Get a docker on your machine. https://hub.docker.com/
After installing it, run docker engine.

1. running rabbitMQ on docker: docker run -d --hostname my-rabbit --name ecomm-rabbit -p 15672:15672 -p 5672:5672 rabbitmq:3-management
(you can get other ports)

2. check logs: docker logs -f e43beb22ca13
('e43beb22ca13' is a example of running container id, you can check for container id with 'docker ps' command)

3. open up browser and go to: domain:port
for example: 'localhost:15672'

4. login with these credentials:
default login: guest
defaul password: guest

change them after successfuly logging in, these are default credentials 

5. add rabbitmq.client nuget package to your project if you haven't already: dotnet add package RabbitMQ.Client --version 6.2.1

