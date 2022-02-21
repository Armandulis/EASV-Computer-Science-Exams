# Solution for CIFAR-10 dataset - images classification
### CIFAR-10
The chosen dataset is CIFAR-10. It’s a dataset that consists of 60000 images, 50000 of them are intended to be used as training data, and another 10000 of them are used as data to test the trained model. These images are 32x32 and colored, which in my opinion made the model a bit harder to train. Out of 50000 training images, 5000 of them were used as model training validation data. There are 10 types of images: Airplane, Automobile, Bird, Cat, Deer, Dog, Frog, Horse, Ship, Truck.

### Solution
Code was written in Jupyter notebook, for training and testing I used Tensorflow and Keras, panda and matplotlib were used for working with data and graphs.
I managed to reach 69% prediction correctness after it is done training. While training it even goes up to 89%.

### Problem
The main task of this project is image classification. Out of the given, images the model has to determine what the image is out of 10 options. The first step that should be taken is normalizing the pixels of the images. This means we need to make the image pixel value to vary between 0 and 1.

The next step is to deal with 3-dimensional images (as they have RGB colors). To deal with this I used MaxPooling2D and Conv2D. This helps us to convert image values to a 2-dimensional array, which will be used by the model to train data.

We also want to categorize (one to hot) our labels. What this means is that we will have an array from 1 to 10 (as there are 10 times of images),  which will be used as a way to tell the model what that image is. So we will end up with an array that contains 9 zeros and 1 one.

Finally, as we might run into very long learning times with no improvements, adding callbacks to a model, such as ReduceLROnPlateau and EarlyStopping, should greatly improve the program.

### Model Architecture
Multilayer perceptron (MLP) was used as the model’s architecture. This architecture consists of layers of neurons, that is one input layer, one output layer, and one or more hidden layers in between them. All layers except the output layer are entirely connected to the next layer. MLP architecture is great for classification prediction problems, which is the main reason why I chose this architecture.

Taken from the report. Report can be found here: https://docs.google.com/document/d/1B-d1pbyZduYZLzfShALeLuj7J8h71_XP4dn88dNP8Ps/edit?usp=sharing
