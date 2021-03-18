#-------------------------------------------------------------------------------
# Name:        module1
# Purpose:
#
# Author:      Celia
#
# Created:     03/12/2020
# Copyright:   (c) Celia 2020
#-------------------------------------------------------------------------------
import numpy as np

import tensorflow as tf
assert tf.__version__.startswith('2')

from tensorflow_examples.lite.model_maker.core.data_util.image_dataloader import ImageClassifierDataLoader
from tensorflow_examples.lite.model_maker.core.task import image_classifier
from tensorflow_examples.lite.model_maker.core.task.model_spec import mobilenet_v2_spec
from tensorflow_examples.lite.model_maker.core.task.model_spec import ImageModelSpec

import matplotlib.pyplot as plt

#../Recursos/Images/ParaRedNeuronal/Train/

train_data = ImageClassifierDataLoader.from_folder('../Recursos/Images/ParaRedNeuronal/Train/')
print('Imágenes de entrenamiento cargadas')

test_data = ImageClassifierDataLoader.from_folder('../Recursos/Images/ParaRedNeuronal/Test/')
#train_data, test_data = data.split(0.9)
print('Imágenes de test cargadas')

model = image_classifier.create(train_data)
print('Modelo entrenado')

loss, accuracy = model.evaluate(test_data)
#print('Test accuracy: %f' % accuracy )

#model.export('Images/')
model.export(export_dir='Images/', with_metadata=False)

print('Modelo exportado')
