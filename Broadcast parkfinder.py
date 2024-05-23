from socket import *
import json
from time import sleep
import random
from datetime import datetime, time

serverName = '255.255.255.255'
serverPort = 11101
clientSocket = socket(AF_INET, SOCK_DGRAM)
clientSocket.setsockopt(SOL_SOCKET, SO_BROADCAST, 1)
sensorName = "Parking_space_sensor"

carsCounter = 0
amountOfCars = 0
maxAmountOfCars = 150

while True:
    currentTime = datetime.now().time()
    carsCounter += 1
    if currentTime.hour < 12 and currentTime.hour > 6:
        amountOfCars += 1
    else:
        amountOfCars += 1
    if amountOfCars > 50 and carsCounter % 5 == 0:
        amountOfCars -= 1
    if amountOfCars > maxAmountOfCars:
        amountOfCars = maxAmountOfCars
    if amountOfCars > 124:
        sleep(random.randint(15,60))
    if amountOfCars > 145:
        sleep(random.randint(300,600))
    else:
        sleep(random.randint(5,15))
    carObject = {"Sensor": sensorName, "New car": amountOfCars, "Max cars": maxAmountOfCars, "Total cars": carsCounter, "Time": currentTime.strftime('%H:%M')}
    message = json.dumps(carObject)
    clientSocket.sendto(message.encode(), (serverName, serverPort))
    