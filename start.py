from server import create_server
from UIInstructions import *
import video_capture
import cv2
import numpy as np
import serialization
from skinclassifier import SkinClassifier
from facetracker import FaceTracker
from rppgsensor import SimplePPGSensor
from signalprocessor import ChrominanceExtracter
from evaluator import Evaluator
from framecapture import WebcamCapture,Stationary
import TextWriter
class Main:
    def __init__(self):
        fs = 24
        self.frameCapture = Stationary(1)
        self.SkinClassifier = SkinClassifier()
        self.faceTracker = FaceTracker()
        self.sensor = SimplePPGSensor(self.frameCapture)
        self.processor = ChrominanceExtracter(self.sensor,self.frameCapture)
        self.evaluator = Evaluator(self.processor)
        



    def main(self):
        frame = self.frameCapture.get_frame()
   
        face = []
        face = self.faceTracker.crop_to_face(frame)
            
        skin,pixels = self.SkinClassifier.apply_skin_classifier(face)
        self.sensor.sense_ppg(skin,pixels)
        self.processor.extract_pulse()
        self.evaluator.evaluate(skin)
        TextWriter.refresh()
        return skin
        
main = Main()
#serialization.LoadFromJson(main.SkinClassifier)
video_capture.main = main
uiInstructions = [
            Slider("maxh","SkinClassifier","Max Hue",0,255,main.SkinClassifier,None),
            Slider("minh","SkinClassifier","Min Hue",0,255,main.SkinClassifier,None),
            Slider("mins","SkinClassifier","Min Saturation",0,255,main.SkinClassifier,None),
            Slider("maxs","SkinClassifier","Max Saturation",0,255,main.SkinClassifier,None),
            Slider("minv","SkinClassifier","Min Value",0,255,main.SkinClassifier,None),
            Slider("maxv","SkinClassifier","Max Value",0,255,main.SkinClassifier,None),         
            Slider("elipse_size","SkinClassifier","Elipse Size",0,20,main.SkinClassifier,None),
            Slider("blursize","SkinClassifier","Blur Size",0,50,main.SkinClassifier,None), 
            Switch("enabled","SkinClassifier","Enabled",main.SkinClassifier,None), 
            
            #TimeFigure(main.SkinClassifier,"t",["num_skin_pixels"],"x",["num_skin_pixels"])                 
            
            Dropdown("frameCapture","VideoSettings","Video Input",main,[Stationary(1),WebcamCapture()],["Stationary","Webcam"],None),
            # Dropdown("selectedCamera","VideoSettings","Selected Camera",main,[1,0],["1","0"],"setCameraUpdate")    
            ]
            
            

if __name__ == '__main__':
    app = create_server(uiInstructions,lambda : video_capture.Camera())
    app.run(debug=True,threaded = True)

