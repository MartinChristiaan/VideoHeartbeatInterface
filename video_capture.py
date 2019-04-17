import cv2
from base_camera import BaseCamera
from pythonsource.main import Main

main = Main()

class Camera(BaseCamera):
    video_source = 1
    stopped = False
    process = lambda frame:frame
    
    @staticmethod
    def set_process(process):
        Camera.process = process

    @staticmethod
    def set_video_source(source):
        Camera.video_source = source
        
    @staticmethod
    def frames():
        camera = cv2.VideoCapture(Camera.video_source)
        camera.set(3, 1280)
        camera.set(4, 720)
        if not camera.isOpened():
            raise RuntimeError('Could not start camera.')

        while True:
            # read current frame
            _, img = camera.read()
            result = main.main(img)     
            # Put update loop here
            # encode as a jpeg image and return it
            yield cv2.imencode('.jpg', result)[1].tobytes()
