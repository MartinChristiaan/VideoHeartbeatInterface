import cv2
import numpy as np
class SkinClassifier:
    def __init__(self,):
        self.lower = np.array([0, 48, 80], dtype = "uint8")
        self.upper = np.array([20, 255, 255], dtype = "uint8")
        self.elipse_size = 12
        self.blursize = 5


    def apply_skin_classifier(self,frame):
        converted = cv2.cvtColor(frame, cv2.COLOR_BGR2HSV)
        skinMask = cv2.inRange(converted, self.lower, self.upper)
        kernel = cv2.getStructuringElement(cv2.MORPH_ELLIPSE, (self.elipse_size, self.elipse_size))
        skinMask = cv2.erode(skinMask, kernel, iterations = 2)
        skinMask = cv2.dilate(skinMask, kernel, iterations = 2)
        skinMask = cv2.GaussianBlur(skinMask, (self.blursize, self.blursize), 0)
    
        num_skin_pixels = skinMask.clip(0,1).sum()
        skin = cv2.bitwise_and(frame, frame, mask = skinMask)
        return skin,num_skin_pixels