from flask import Flask
from flask import render_template
import os
from os import path
import json


inputDataPath = "input.json"




# def recursiveDeserialize(newdict,savedDict):
#     for item in newdict.items():
#         if type(item[1]) == float or type(item[1]) == int  :         
#             if not savedDict.__contains__(item[0]):
#                 savedDict[item[0]] = (item[1],0,1)
#             newdict[item[0]] = savedDict[item[0]][0]
#         else:
#             if not savedDict.__contains__(item[0]):
#                 savedDict[item[0]] = recursiveDeserialize(item[1].__dict__,dict())
#             else: 
#                 savedDict[item[0]] = recursiveDeserialize(item[1].__dict__,savedDict[item[0]])
#             newdict[item[0]] = savedDict[item[0]]
#     unwantedkeys = []
#     for key in savedDict.keys():
#         if not newdict.__contains__(key):
#             unwantedkeys.append(key)
    
#     for key in unwantedkeys:
#         savedDict.pop(key,None)
#             # Remove unused keys
#     return savedDict



def load_inputData(inputs):

    savedDict = dict()
    categories = dict()

    try:
        f= open(inputDataPath,'r')
        savedDict = json.loads(f.read())
        f.close()
        print("Found inputdata ")
      
    except:
        print("Input settings not found, Creating new")
    
    inputDict = inputs.__dict__
    updatedItems = set()
    for category in inputDict.items():
        subdict = category[1].__dict__
        categories[category[0]] = len(subdict)
        for item in subdict.items():
            if not savedDict.__contains__(item[0]):
                savedDict[item[0]] = (item[1],0,1)
            # set loaded item
            subdict[item[0]] = savedDict[item[0]][0]
            updatedItems.add(item[0])
    
    unwantedkeys = []
    for key in savedDict.keys():
        if not updatedItems.__contains__(key):
            unwantedkeys.append(key)
    
    for key in unwantedkeys:
        savedDict.pop(key,None)
    return savedDict,categories 
    



    #return recursiveDeserialize(inputs.__dict__,inputData)


    

def saveInputs(inputData):

    jsontring = json.dumps(inputData)
    f= open(inputDataPath,'w')
    f.write(jsontring)
    f.close()





