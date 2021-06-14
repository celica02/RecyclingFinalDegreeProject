#-------------------------------------------------------------------------------
# Name:        module1
# Purpose:
#
# Author:      celia
#
# Created:     13/06/2021
# Copyright:   (c) celia 2021
# Licence:     <your licence>
#-------------------------------------------------------------------------------
from __future__ import absolute_import
from __future__ import division
from __future__ import print_function
import numpy as np

import tensorflow as tf
assert tf.__version__.startswith('2')

from tensorflow_examples.lite.model_maker.core.data_util.image_dataloader import ImageClassifierDataLoader
from tensorflow_examples.lite.model_maker.core.task import image_classifier
#from tensorflow_examples.lite.model_maker.core.task.model_spec import mobilenet_v2_spec
#from tensorflow_examples.lite.model_maker.core.task.model_spec import ImageModelSpec

from tensorflow_examples.lite.model_maker.core.task import classification_model

import tensorflow as tf
##import numpy as np
from PIL import Image


class TensorflowLiteClassificationModel:
    def __init__(self, model_path, labels, image_size=224):
        self.interpreter = tf.lite.Interpreter(model_path=model_path)
        self.interpreter.allocate_tensors()
        self._input_details = self.interpreter.get_input_details()
        self._output_details = self.interpreter.get_output_details()
        self.labels = labels

##        height = self._input_details[0]['shape'][1]
##        width = self._input_details[0]['shape'][2]
##        image_np = cv2.resize(image_np, (height, width))
        self.image_size=image_size

    def run_from_filepath(self, image_path):
        input_data_type = self._input_details[0]["dtype"]
        image = np.array(Image.open(image_path).resize((self.image_size, self.image_size)), dtype=input_data_type)
        if input_data_type == np.float32:
            image = image / 255.

        if image.shape == (1, 224, 224):
            image = np.stack(image*3, axis=0)

        return self.run(image)

    def run(self, image):
        """
        args:
          image: a (1, image_size, image_size, 3) np.array

        Returns list of [Label, Probability], of type List<str, float>
        """
        image = tf.io.read_file(image)
        tf.io.decode_image(image, channels=3)
        self.interpreter.set_tensor(self._input_details[0]["index"], image)
        self.interpreter.invoke()
        tflite_interpreter_output = self.interpreter.get_tensor(self._output_details[0]["index"])
        probabilities = np.array(tflite_interpreter_output[0])

        # create list of ["label", probability], ordered descending probability
        label_to_probabilities = []
        for i, probability in enumerate(probabilities):
            label_to_probabilities.append([self.labels[i], float(probability)])
        return sorted(label_to_probabilities, key=lambda element: element[1])


def main():
##    test_data = ImageClassifierDataLoader.from_folder('../Recursos/Images/ParaRedNeuronal/Test/')
##    #train_data, test_data = data.split(0.9)
##    print('Imágenes de test cargadas')
##
##    loss, accuracy = evaluate_tflite_rec(self, '../ResultadosEntrenamientos/LatasRealesYGenradas/1000/1000R_0G/1000Rmodel.tflite', '../Recursos/Images/ParaRedNeuronal/Test/')
##    print(accuracy)

##    interpreter = tf.lite.Interpreter("../ResultadosEntrenamientos/LatasRealesYGenradas/1000/1000R_0G/1000Rmodel.tflite")
##    interpreter.allocate_tensors()

#_______________________
##    test_data = ImageClassifierDataLoader.from_folder('../Recursos/Images/ParaRedNeuronal/Test/')
##    interpreter = tf.lite.Interpreter("../ResultadosEntrenamientos/LatasRealesYGenradas/1000/1000R_0G/1000Rmodel.tflite")
##    interpreter.allocate_tensors()
##    input_details = interpreter.get_input_details()
##    output_details = interpreter.get_output_details()
##
##    interpreter.allocate_tensors()
##    input_data = np.expand_dims(new_img, axis=0).astype(np.float32)
##    interpreter.set_tensor(input_details[0]['index'], input_data)
##
##    interpreter.invoke()
##
##    image = tf.io.read_file("../Recursos/Images/ParaRedNeuronal/Test/Lata/AluCan1,000.jpg")
##    tf.io.decode_image(image, channels=3)
#___________________________________________

    model = TensorflowLiteClassificationModel("../ResultadosEntrenamientos/LatasRealesYGenradas/1000/1000R_0G/1000Rmodel.tflite", "../ResultadosEntrenamientos/LatasRealesYGenradas/1000/1000R_0G/labels.txt")
    #loss, accuracy = interpreter.evaluate(test_data)

    ClassificationModel.evatuale_tflite()

    (label, probability) = model.run_from_filepath("../Recursos/Images/ParaRedNeuronal/Test/Lata/AluCan1,000.jpg")


main()
