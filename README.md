# Unity Editor Scripts Toolchain
My daily use Unity Editor Scripts consolidated into one window
## Generate Prefabs
![image](https://github.com/dguliev-github/unity_toolchain/assets/64034875/ed4a2ac7-5ca6-43be-8542-8eb5f9bd71fa)
![image](https://github.com/dguliev-github/unity_toolchain/assets/64034875/2636110c-920b-4f6b-9da4-298b1d0e5449)

Generate Prefabs from Selected Object's Children Meshes. 
### Requirements:
* Select gameObject from the Hierarchy or Project window.
* Each affected child has to have mesh filter component. 
### Notes:
* Prefabs are named according to the object from which they were generated.
* Prefab transforms are set to (0,0,0).
### Typical usage scenario:
One-click to generate prefab kit from fbx file objects, made in 3d software.
## Swap materials
![image](https://github.com/dguliev-github/unity_toolchain/assets/64034875/95da3351-44c8-4830-b2bd-5eb4c8c8ca86)

Swaps first material with the second one in all selected mesh renderers
### Requirements:
* Select gameobjects on which material will be swapped.
* Set material to remove (old)
* Set material to replace with (new)
### Typical usage scenario:
Change seamless material from one to another, create more gameobject variations.
## Replace with prefab
![image](https://github.com/dguliev-github/unity_toolchain/assets/64034875/6292e340-a691-457f-997c-e176e2b2f0dd)

Swaps selected gameobjects with the particular prefab. Saves original transforms.
### Requirements:
* Select gameObjects from the Hierarchy.
* Select prefab to swap with.
### Notes:
* Mostly deprecated after Unity 2022.3.x, which already includes ways to swap prefabs.
### Typical usage scenario:
Useful for converting propotyping blockout to the final objects, as well as swapping the same objects with their variations.
## Select with the same material
![image](https://github.com/dguliev-github/unity_toolchain/assets/64034875/3ec2df31-0495-4cdb-bc5c-01982e777745)

Selects all objects with the chosen material.
### Requirements:
* Select material from the Project window.
### Notes:
* Works the same way as "Find references in scene" context-menu option, but can be more convenient to use when paired with Swap materials tools.
### Typical usage scenario:
Batch find-and-swap particular material.

## Copy Transforms
![image](https://github.com/dguliev-github/unity_toolchain/assets/64034875/c7513f20-5ad6-4a68-bf84-93e2716be547)

Recursively copies transforms including children with the same name.
### Requirements
* Set Donor object (from which transforms will be copied)
* Set Target object (to which transforms will be pasted)
### Typical usage scenario:
Copy transforms of unpackaged fbx prefab (including child objects) to the original packaged fbx prefab to restore link with fbx file.

## Select children
![image](https://github.com/dguliev-github/unity_toolchain/assets/64034875/1e0c98f6-b233-4b59-9009-07cfa907b2d9)

Selects children of all selected parent objects
### Requirements
* Select objects from hierarchy window.

## Copy Path
![image](https://github.com/dguliev-github/unity_toolchain/assets/64034875/e9f1e4b3-4f6c-409d-a4b1-350f975e4c92)

Creates additional option in Project window context menu. I included it for convenience.
Copyright Alan Mattano @ SOARING STARS Lab

# Installation
## Option 1

Requires GIT installation

Window > Package Manager > + > Add package from git url > `https://github.com/dguliev-github/unity_toolchain.git`

## Option 2
Download as .zip > unpack anywhere in `/Assets/` folder of your project.

# Usage

Tools > DG's Toolchain

Dock window anywhere you like.

# Customizing
You can easily add or remove any Editor Script from the toolchain.
### Requirements for the Editor Scripts
* They have to be inherited from the `EditorWindow`
* Make `static void ShowWindow()` public
* make `void OnGUI()` public
### Required changes for the DGs_Toolchain.cs
* Expand `toolOptions` array with the name of your tool. It will be visible in the drop-down menu.
* Add `private YourEditorScript yourEditorScriptInstance` variable
* Instantiate your Editor Script Instance in `OnEnable()`
* Add additional case for `selectedToolIndex`
