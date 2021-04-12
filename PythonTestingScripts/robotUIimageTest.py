
import asyncio
import websockets
from io import StringIO, BytesIO
from PIL import Image
import base64

im = Image.open("images/ScrewdriverCut.png")
im2 = Image.open("images/ScrewdriverTable.png")

async def hello():
    uri = "ws://localhost:8765"
    async with websockets.connect(uri) as websocket:
        fp = BytesIO()
        im.save(fp, 'PNG')
        fp2 = BytesIO()
        im2.save(fp2, 'PNG')
        await websocket.send("imgs||"+base64.b64encode(fp.getvalue()).decode()+"||"+base64.b64encode(fp2.getvalue()).decode())
        fp.close()
        fp2.close()

            
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