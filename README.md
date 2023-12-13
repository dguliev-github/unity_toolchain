# Unity Editor Scripts Toolchain
My daily use Unity Editor Scripts consolidated into one window. Expandable.
## Swap materials
![image](https://github.com/dguliev-github/unity_toolchain/assets/64034875/95da3351-44c8-4830-b2bd-5eb4c8c8ca86)

Swaps first material with the second one in all selected mesh renderers

**Requirements:**

* Select gameobjects on which material will be swapped.
* Set material to remove (old)
* Set material to replace with (new)

**Typical usage scenario:**

Change seamless material from one to another, create more gameobject variations.
## Replace with prefab
![image](https://github.com/dguliev-github/unity_toolchain/assets/64034875/6292e340-a691-457f-997c-e176e2b2f0dd)

Swaps selected gameobjects with the particular prefab. Saves original transforms.

**Requirements:**

* Select gameObjects from the Hierarchy.
* Select prefab to swap with.

**Notes:**

* Mostly deprecated after Unity 2022.3.x, which already includes ways to swap prefabs.

**Typical usage scenario:**

Useful for converting propotyping blockout to the final objects, as well as swapping the same objects with their variations.
## Select with the same material
![image](https://github.com/dguliev-github/unity_toolchain/assets/64034875/3ec2df31-0495-4cdb-bc5c-01982e777745)

Selects all objects with the chosen material.

**Requirements:**

* Select material from the Project window.

**Notes:**

* Works the same way as "Find references in scene" context-menu option, but can be more convenient to use when paired with Swap materials tools.

**Typical usage scenario:**

Batch find-and-swap particular material.

## Copy Transforms
![image](https://github.com/dguliev-github/unity_toolchain/assets/64034875/c7513f20-5ad6-4a68-bf84-93e2716be547)

Recursively copies transforms including children with the same name.

**Requirements**
* Set Donor object (from which transforms will be copied)
* Set Target object (to which transforms will be pasted)

**Typical usage scenario:**

Copy transforms of unpackaged fbx prefab (including child objects) to the original packaged fbx prefab to restore link with fbx file.

## Select children
![image](https://github.com/dguliev-github/unity_toolchain/assets/64034875/1e0c98f6-b233-4b59-9009-07cfa907b2d9)

Selects children of all selected parent objects.

## Transfer Colliders
![image](https://github.com/dguliev-github/unity_toolchain/assets/64034875/8d547f06-30e8-46f2-8632-73681c9485f5)

Allows to re-attach collider components between parent and child gameobjects. If you need to transfer colliders to a child you have to select this child. If you need to transfer colliders to a parent you have to select a parent respectively.

**Requirements**

Selected gameobject has to have parent/child with collider components. 

**Typical usage scenario:**

You added colliders to the wrong gameobject and have to redo them on another one. Transfer them to child instead and re-parent it to the right one. You can then reattach colliders to the new parent using the reversed operation.

## Set LOD Groups
![image](https://github.com/dguliev-github/unity_toolchain/assets/64034875/fb4efb72-42e8-45db-a58a-d1b195366d46)

Add and automatically setup LOD group components. Does exactly the same as LOD group postprocessor but allows you to choose where you want this component to be.

**Requirements**

See the example below. You have to select gameobjects on which you want your LOD group component. In the example they would be prop_1, prop_2, etc.

**Typical usage scenario:**

You have an fbx file with hierarchy like that:
* props
  * prop_1
      * prop_1_LOD0
      * prop_1_LOD1
      * prop_1_LOD2
  * prop_2
      * prop_2_LOD0
      * prop_2_LOD1
      * prop_2_LOD2
  * ...
  * ...
   * prop_n
      * prop_n_LOD0
      * prop_n_LOD1
      * prop_n_LOD2

 You need to create separate prefabs from every parent gameobject, with LOD group component on every of them. Unfortunately, by default Unity adds LOD group component to the main parent of an imported file, it this case it's `props`.

 This script allows you to re-setup LOD groups and then generate independent prefabs with ease.

## Set LOD Transitions
![image](https://github.com/dguliev-github/unity_toolchain/assets/64034875/5a5396d9-9cb4-46f0-a9ef-5d9ade84f102)

Automatically sets LOD transition value by interpolating between LOD0 and last LOD value. Useful when LOD count between objects is inconsistent but you need them to cull on the exact same distance or screen height percent.

**Requirements**

Select gameobjects with the LOD Group components and set LOD0 transition percent and the last LOD transition percent. The first value should be bigger than the second. 

**Note**

This is how the resulting gameobjects with one, two, three, and four LOD groups would look like in the inspector:

![image](https://github.com/dguliev-github/unity_toolchain/assets/64034875/beedbd12-826b-4b4a-a541-732b2eb2a11f)

## Copy Path
![image](https://github.com/dguliev-github/unity_toolchain/assets/64034875/e9f1e4b3-4f6c-409d-a4b1-350f975e4c92)

Creates additional option in Project window context menu. I included it for convenience.

It copies absolute path of the selected file or folder to the clipboard. Useful for pasting export directory in various asset creation suites.

Copyright Alan Mattano @ SOARING STARS Lab

## Installation
### Option 1

Requires GIT installation

Window > Package Manager > + > Add package from git url > `https://github.com/dguliev-github/unity_toolchain.git`

### Option 2
Download as .zip > unpack anywhere in `/Assets/` folder of your project. As editor script, the package contents should be in a folder named `Editor`.

## Usage

`Menu > Tools > DG's Toolchain`

![image](https://github.com/dguliev-github/unity_toolchain/assets/64034875/b97101e4-a59a-4c26-83e8-a3e1eac8880e)

Dock window anywhere you like.

## Customizing
You can easily add or remove any Editor Script from the toolchain.

**Requirements for the Editor Scripts**

* They have to be inherited from the `EditorWindow`
* `void OnGUI()` should be static public. All corresponding methods should be static as well.

**Required changes for the DGs_Toolchain.cs**

* Expand `toolOptions` array with the name of your tool. It will be visible in the drop-down menu.
* Add additional case for `selectedToolIndex`
