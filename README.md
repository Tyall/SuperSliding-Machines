<h1 align="center"> SuperSliding Machines </h1> <br>
<p align="center">
    <img src="https://github.com/Tyall/SuperSliding-Machines/blob/master/images/ssm.png" width="450">
</p>
A 3D mobile racing game with AI based on artificial neural networks created in Unity. Started as a hobby project, finished as my engineering thesis topic.
Graphic design is characterized by low poly models and isometric camera view. The aim was to create a simple racing game, accessible to everyone, with a 
difficulty of gameplay highly dependent on the usage of artificial intelligence.

## Game

### Main Menu
Menu is based on a single 3D scene. Access to its functionalities is provided by camera transitions to chosen points of interest.

![](https://github.com/Tyall/SuperSliding-Machines/blob/master/images/ssm_main_menu.jpg)

### Workshop
In the workshop player can customize vehicle to suit his preferences.

![](https://github.com/Tyall/SuperSliding-Machines/blob/master/images/ssm_repaint.jpg)

### Dealership
Dealership allows player to browse and buy new vehicles to the collection with credits earned through racing.

![](https://github.com/Tyall/SuperSliding-Machines/blob/master/images/ssm_dealership.jpg)

### Collection
A player's own virtual garage containing bought and customized vehicles, ready be selected to race!

![](https://github.com/Tyall/SuperSliding-Machines/blob/master/images/ssm_collection.jpg)

### Level Selection
Player can chose a level from the unlocked ones. As you progress through the game, more levels will be unlocked.

![](https://github.com/Tyall/SuperSliding-Machines/blob/master/images/ssm_level_selection.jpg)

### Coast City Race
A showcase of the first of two longest races in the game. Based in a city with tight roads and lots of buildings in the neighbourhood.

![](https://github.com/Tyall/SuperSliding-Machines/blob/master/images/ssm_race9.jpg)

### Green Hell Race
A showcase of the second of two longest races in the game. Located in the hills of the green mountain, a real challenge for every player!

![](https://github.com/Tyall/SuperSliding-Machines/blob/master/images/ssm_race13.jpg)

### Race view
A view showing camera and user interface through the ongoing race.

![](https://github.com/Tyall/SuperSliding-Machines/blob/master/images/ssm_race.jpg)

## Artificial Intelligence

Opponent vehicles are controlled by artificial neural network which results in their behaviour being close to realistic. Artificial Neural Network (ANN) needs to be taught 
first in order to make it work. The process of learning is realised by the genetic algorithm. Such an approach is commonly refered as hybrid model of artificial intelligence,
in this case called Artificial Neural Network - Genetic Algorithm (ANN-GA)

### AI Learning Environment

An environment dedicated to train neural networks separately is included in the project and can be accessed through the Unity editor. You can experiment with the shape of
artificial neural network and parameters of learning by modifing genetic algorithm related values. You can also have fun by observing how the evolution process works!
![](https://github.com/Tyall/SuperSliding-Machines/blob/master/images/ssm_ai_earning_environment.jpg)



## Made with 
- Unity3D
- Visual Studio Code
- Blender
- GIMP
- Lots of great, free to use Unity libraries.


## Features
- Chose a vehicle from 10 available models
- Race through the campaign
- Collect experience and coins to upgrade your vehicles
- Customize your vehicle
- Make your own collection of a various machines
- Improve your driving skills
- Beat AI with progressing difficulty

## Installation 
Clone this repo to your desktop and import it to Unity editor. A version of 2019.4 or later is required in order to get all components to work.

## Usage 
After you open this project in Unity, build an apk package. Install it on desired android mobile device or an emulator and simply launch the game. 
Make sure to match the minimum system requirements to enjoy the game!

## Minimum System requirements
- Android 4.4 (API 19+)
- 2GB RAM
- ARMv7 or ARM64 CPU architecture
- Support of OpenGL ES 2.0+ or Vulkan
