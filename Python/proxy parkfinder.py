import socket
import json
import requests

ServerName = '127.0.0.1'
serverport = 11101

address = (ServerName, serverport)

serversocket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
serversocket.setsockopt(socket.SOL_SOCKET, socket.SO_BROADCAST, 1)

serversocket.bind((address))

print('The server is ready')

while True:
    data, addr = serversocket.recvfrom(2048)
    data = data.decode()
    print('Received: ' + data)
    sales = json.loads(data)
    print('id: ' + str(sales['id']))
    print('ledig parkeringsplads: ' + str(sales['ledig parkeringsplads']))
    # response = requests.post("https://charlottesstock.azurewebsites.net/", json=sales)