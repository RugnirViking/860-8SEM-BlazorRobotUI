#!/usr/bin/env python

import asyncio
import websockets

async def hello():
    uri = "ws://localhost:8765"
    async with websockets.connect(uri) as websocket:
        await websocket.send("rob||Hello world!")
        await websocket.send("usr||Hello robot friend :)")
        await websocket.send("rob||How are you doing today?")
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