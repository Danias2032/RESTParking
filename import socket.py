from socket import *
import requests

serverPort = 11101
serverSocket = socket(AF_INET, SOCK_DGRAM)
serverAddress = ('', serverPort)

#apiAddress = "http://"
#headersArray = {'ContentType': 'application/json'}

serverSocket.bind(serverAddress)
print("Server - Ready")

while True:
    message, clientAddress = serverSocket.recvfrom(2048)
    print("Received message:" + message.decode())
    print("Message sent to client address:", clientAddress)
    #requests.post(apiAddress, data=message, headers=headersArray)