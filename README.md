ROUTER_TEXTURE - a model object which was created in blender and imported to unity with textures. the mesh was applied textures on its sides  
Cube - we'll change its dimensions and texture using script
DynamicBackgroundColor - empty object to make DynamicBackgroundColor.cs script work
Assets/Scripts/  
RotateModel.cs - script was applied on to ROUTER_TEXTURE to rotate it with drag  
ZoomInOut.cs - script was applied to the camera to capture the mouse wheel and to adjust the camera towards and away from the object to make the zoom effect  
DynamicBackgroundColor.cs- this can recieve values from react native using https://react-unity-webgl.dev/   
DeviceModelLoader.cs - the dimensions and image urls are hardcoded here at the moment, need to get it from react native or an api and it can modify the cube scale and apply image textures on sides