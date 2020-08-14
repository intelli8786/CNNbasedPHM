CNNbasedPHM

![Model](https://user-images.githubusercontent.com/25438139/90269985-75bbf600-de94-11ea-9880-28366586303d.png)
This repository contains the official implementation of the following paper:
> **CNN based Actuator Fault Cause Classification System Using Noise**<br>
>
> *This paper proposes a PHM(Prognostics and Health Monitoring) system based on CNN(Convolutional Neural Network). Our approach aims at diagnosing the state of the machine in real time after extracting the features of the sound generated from the machine and then learning a CNN-based sound classification model. The advantage of our approach is that it is not machine-specific and can be easily applied to a variety of machines to do PHM. To achieve the purpose, we designed a chain model driven by a motor, and the sound source extracted from it was used for learning our sound classification model to derive experimental results.*

# Usage
## Development Environment
* CPU : i7 8086k
* GPU : NVIDIA GeForce RTX2080ti
* Windows 10 1903
* Lattepanda DFR0547
* Dynamic mic
## Dependency
* dotnet framework 4.5.1
* MathConverter 1.2.1.1
* NAudio 1.8.2
* Newtonsoft.Json 10.0.3
* python 3.6
* jupyter 5.0.0
* tensorflow-gpu 1.9.0
* librosa 0.8.0
* numpy 1.16.1
* opencv 4.2.0

## Proposed system
### Demonstration video
[![](https://user-images.githubusercontent.com/25438139/90269991-76ed2300-de94-11ea-9d67-fc3b4a758ede.PNG)](https://youtu.be/a-PG5FFXicw)

### Management console
![ManagementConsole](https://user-images.githubusercontent.com/25438139/90269996-7785b980-de94-11ea-82a4-6dc9f7836fbd.PNG)
