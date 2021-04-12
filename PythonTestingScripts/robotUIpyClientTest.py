#!/usr/bin/env python

import asyncio
import websockets
from io import StringIO
from PIL import Image
step=0;

async def hello():
    global step
    uri = "ws://localhost:8765"
    print(step)
    if step==0:
        step=1
        async with websockets.connect(uri) as websocket:
            await websocket.send("rob||Hello world!")
    elif step==1:
        step=2
        async with websockets.connect(uri) as websocket:
            await websocket.send("usr||Hello robot friend :)")
    elif step==2:
        step=3
        async with websockets.connect(uri) as websocket:
            await websocket.send("rob||How are you doing today?")
    elif step==3:
        step=0
        async with websockets.connect(uri) as websocket:
            await websocket.send("usr||I am doing fine thank you robot. I hope we can work together effectively.")



@asyncio.coroutine
def periodic():
    while True:
        yield from hello()
        yield from asyncio.sleep(1)

def stop():
    task.cancel()

loop = asyncio.get_event_loop()
loop.call_later(25, stop)
task = loop.create_task(periodic())

try:
    loop.run_until_complete(task)
except asyncio.CancelledError:
    pass