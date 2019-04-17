#!/usr/bin/env python
from importlib import import_module
import os
from flask import Flask, render_template, Response ,request
from flask_cors import CORS
import video_capture 

# Raspberry Pi camera module (requires picamera package)
# from camera_pi import Camera

class Settings:
    def __init__(self):
        self.stopped = False
        self.prev_frame = []
app = Flask(__name__)
CORS(app)
settings = Settings()

@app.route('/')
def index():
    return ""

def gen(camera):
    """Video streaming generator function."""
    while True:
        frame = camera.get_frame()
        if settings.stopped:    
            frame = settings.prev_frame
        else:
            settings.prev_frame = frame
        yield (b'--frame\r\n'
            b'Content-Type: image/jpeg\r\n\r\n' + frame + b'\r\n')


@app.route('/video_feed')
def video_feed():
    """Video streaming route. Put this in the src attribute of an img tag."""
    return Response(gen(video_capture.Camera()),
                    mimetype='multipart/x-mixed-replace; boundary=frame')

@app.route('/toggleStop')
def toggleStop():
    settings.stopped = not settings.stopped
    return ""
@app.route('/updateClassifier')
def updateClassifier(methods=['PUT']):
    data = request.form["data"]
    print("data")
    values = [float(el) for el in data.split()]  
    video_capture.main.skinClassifier.updateClassifier(values)
    return ""


if __name__ == '__main__':
    app.run(debug = True,threaded=True)
