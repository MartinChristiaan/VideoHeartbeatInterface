from pythonsource.skinclassifier import SkinClassifier
class Main:
    def __init__(self):
        self.skinClassifier = SkinClassifier()
    
    def main(self,frame):
        skin,num_skinpixels = self.skinClassifier.apply_skin_classifier(frame)
        
        return skin

 

