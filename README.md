Slayer-II-Particle-System
=========================

This repo contains all the code for the Particle System used in the Slayer II Particle Editor.

Getting Started
=========================

Pre-compiled DLLS for both the Particle System and the File System can be found in the folder marked dlls. If you wish to compile them yourself, the projects for both of these are located in the root directory of the repo.

The whiteCircle.png file is included with the Particle System project. Make sure that this asset is included in your XNA project's Content Pipeline in the root directory or the particle system will not be able to properly draw the default asset. If you want to change the default asset, look inside of the LoadContent function of the DynamicParticleEmitter Class to make the changes appropriately.

Feel free to make any changes to anything in either the Particle System or File System to better suite your needs. If you have any fixes or updates you wish to see in the project, let me know!

Extra Info
=========================

Check out the Youtube video showing off the Editor's features here: https://www.youtube.com/watch?v=RzRgKscJLVw

Check out the Youtube video showing you how to integrate this Particle System and the .emitter files from the Particle Editor into your game here: https://www.youtube.com/watch?v=s9YsGtGc-II