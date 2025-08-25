import asyncio
import websockets
import json

connected = set()

async def handler(websocket, path):
    # Register client
    connected.add(websocket)
    try:
        async for message in websocket:
            # Echo received message to all clients
            for conn in connected:
                if conn != websocket:
                    await conn.send(message)
    except websockets.ConnectionClosed:
        pass
    finally:
        connected.remove(websocket)

async def main():
    async with websockets.serve(handler, "0.0.0.0", 10000):
        print("Server running on ws://0.0.0.0:10000")
        await asyncio.Future()  # run forever

if __name__ == "__main__":
    asyncio.run(main())