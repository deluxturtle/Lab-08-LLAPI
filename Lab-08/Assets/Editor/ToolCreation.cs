using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class ToolCreation : MonoBehaviour {

	[MenuItem("Project Tools/Create Structure")]
	public static void CreateStructure()
	{
		AssetDatabase.CreateFolder ("Assets", "DynamicAssets");
		string dynamicAssets = "These are assets that are loaded in dynamically via Resources.Load(...)\n" +
			"Everything should be places inside the Resources folder, nothing should be in the Dynamic Assets folder\n" +
				"Animations Workflow:\n" +
				"\tInside of the animations folder, create a folder for EACH animation\n" +
				"\tInside of the Animatinos/Sources folder, create a folder for EACH animation\n" +
				"\tThen, import the direct model into the appropriate sources folder. For example, the .obj, .fbx, .blend, .maya, ect.\n" +
				"\tAfter the import, copy the .anim files from the source folder into the Animations/appropriate animation\n" +
				"\tAfter the .anim files are placed, create the animation controller for the object and place it directly into the AnimatorControllers folder\n" +
				"Effects Workflow:\n" +
				"\tCreate a folder with the name of the particle effect\n" +
				"\tPlace all resources for the particle effect in the appropriate folder\n" +
				"Models Workflow:\n" +
				"\tThese are raw unanimated models or models with a single animation\n" +
				"\tCharacters are for other characters that run on set animations, or are unanimated (such as frogs)\n" +
				"\tEnviornment is for enviornmental objects, such as logs, trees, ect.\n" +
				"Prefabs Workflow:\n" +
				"\tThis folder is for storing all prefabs\n" +
				"\tThis folder should be seperated like so:\n" +
				"\t\tCommon: Prefabs that are common between multiple scenes\n" +
				"\t\tLevelOne: Prefabs that are only found in level one. ONLY found in level one. ONLY.\n" +
				"Sounds Workflow:\n" +
				"\tThis folder is for storing all sounds\n" +
				"\tThis folder should be seperated like so:\n" +
				"\tMusic: This is for background music, ect.\n" +
				"\t\tCommon: Sounds that are common between multiple scenes\n" +
				"\t\tLevelOne: Sounds that are only found in level one. ONLY found in level one. ONLY.\n" +
				"\tSFX: This is for special effects, one shots, ect.\n" +
				"\t\tCommon: Sounds that are common between multiple scenes\n" +
				"\t\tLevelOne: Sounds that are only found in level one. ONLY found in level one. ONLY.\n" +
				"Texture Workflow:\n" +
				"\tThis folder is for storing all textures\n" +
				"\tThis folder should be seperated like so:\n" +
				"\t\tCommon: Textures that are common between multiple scenes\n" +
				"\t\tLevelOne: Textures that are only found in level one. ONLY found in level one. ONLY.\n";
		System.IO.File.WriteAllText (Application.dataPath + "/DynamicAssets/FolderStructure.txt", dynamicAssets); 

		AssetDatabase.CreateFolder ("Assets/DynamicAssets", "Resources");
			AssetDatabase.CreateFolder ("Assets/DynamicAssets/Resources", "Animations");
				AssetDatabase.CreateFolder ("Assets/DynamicAssets/Resources/Animations", "Sources");

			AssetDatabase.CreateFolder ("Assets/DynamicAssets/Resources", "AnimatorControllers");

			AssetDatabase.CreateFolder ("Assets/DynamicAssets/Resources", "Effects");

			AssetDatabase.CreateFolder ("Assets/DynamicAssets/Resources", "Models");
				AssetDatabase.CreateFolder ("Assets/DynamicAssets/Resources/Models", "Character");
				AssetDatabase.CreateFolder ("Assets/DynamicAssets/Resources/Models", "Enviornment");

			AssetDatabase.CreateFolder ("Assets/DynamicAssets/Resources", "Prefabs");
				AssetDatabase.CreateFolder ("Assets/DynamicAssets/Resources/Prefabs", "Common");

			AssetDatabase.CreateFolder ("Assets/DynamicAssets/Resources", "Sounds");
				AssetDatabase.CreateFolder ("Assets/DynamicAssets/Resources/Sounds", "Music");
					AssetDatabase.CreateFolder ("Assets/DynamicAssets/Resources/Sounds/Music", "Common");
				AssetDatabase.CreateFolder ("Assets/DynamicAssets/Resources/Sounds", "SFX");
					AssetDatabase.CreateFolder ("Assets/DynamicAssets/Resources/Sounds/SFX", "Common");
			
			AssetDatabase.CreateFolder ("Assets/DynamicAssets/Resources", "Textures");
				AssetDatabase.CreateFolder ("Assets/DynamicAssets/Resources/Textures", "Common");


		AssetDatabase.CreateFolder ("Assets", "Extensions");
		string extensions = "This folder is for the automatic import of asset packages.\n" +
							"Place asset package folders in this folder\n" +
							"Do not place the standard assets in this folder!";
		System.IO.File.WriteAllText (Application.dataPath + "/Extensions/FolderStructure.txt", extensions); 

		AssetDatabase.CreateFolder ("Assets", "Plugins");
		string plugins = "This folder is for the automatic import of plugins.\n" +
							"Place plugin folders and scripts in this folder\n" +
							"Do not place the standard assets in this folder!";
		System.IO.File.WriteAllText (Application.dataPath + "/Plugins/FolderStructure.txt", plugins); 

		AssetDatabase.CreateFolder ("Assets", "Gizmos");
		string gizmos = "This folder is for Gizmo Scripts\n" +
						"There is no inherit organization for this folder";
		System.IO.File.WriteAllText (Application.dataPath + "/Gizmos/FolderStructure.txt", gizmos); 

		AssetDatabase.CreateFolder ("Assets", "Scripts");
		string scripts = "This folder is where all scripts (except gizmo, plugins, extensions, and shaders) are stored.\n" +
						 "Scripts workflow:\n" +
					     "\tCommon: This is where you place scripts that are common across multiple objects or scenes.\n" +
						 "\nNew folders should be created for types of objects or scenes, for example Weapons, Characters, Characters/Birds, StartMenu, ect.";	
		System.IO.File.WriteAllText (Application.dataPath + "/Scripts/FolderStructure.txt", scripts); 
		AssetDatabase.CreateFolder ("Assets/Scripts","Common");
		
		AssetDatabase.CreateFolder ("Assets", "Shaders");
		string shaders = "This folder is for Shader Scripts\n" +
						 "Scripts workflow:\n" +
					     "\tCommon: This is where you place scripts that are common across multiple objects or scenes.\n" +
						 "\nNew folders should be created for types of objects or scenes, for example Weapons, Characters, Characters/Birds, StartMenu, ect.";	
		System.IO.File.WriteAllText (Application.dataPath + "/Shaders/FolderStructure.txt", shaders); 

		AssetDatabase.CreateFolder ("Assets", "StaticAssets");
		string staticAssets = "These are assets that not loaded in dynamically\n" +
				"Animations Workflow:\n" +
				"\tInside of the animations folder, create a folder for EACH animation\n" +
				"\tInside of the Animations/Sources folder, create a folder for EACH animation\n" +
				"\tThen, import the direct model into the appropriate sources folder. For example, the .obj, .fbx, .blend, .maya, ect.\n" +
				"\tAfter the import, copy the .anim files from the source folder into the Animations/appropriate animation\n" +
				"\tAfter the .anim files are placed, create the animation controller for the object and place it directly into the AnimatorControllers folder\n" +
				"Effects Workflow:\n" +
				"\tCreate a folder with the name of the particle effect\n" +
				"\tPlace all resources for the particle effect in the appropriate folder\n" +
				"Models Workflow:\n" +
				"\tThese are raw unanimated models or models with a single animation\n" +
				"\tCharacters are for other characters that run on set animations, or are unanimated (such as frogs)\n" +
				"\tEnviornment is for enviornmental objects, such as logs, trees, ect.\n" +
				"Prefabs Workflow:\n" +
				"\tThis folder is for storing all prefabs\n" +
				"\tThis folder should be seperated like so:\n" +
				"\t\tCommon: Prefabs that are common between multiple scenes\n" +
				"\t\tLevelOne: Prefabs that are only found in level one. ONLY found in level one. ONLY.\n" +
				"Sounds Workflow:\n" +
				"\tThis folder is for storing all sounds\n" +
				"\tThis folder should be seperated like so:\n" +
				"\tMusic: This is for background music, ect.\n" +
					"\t\tCommon: Sounds that are common between multiple scenes\n" +
						"\t\tLevelOne: Sounds that are only found in level one. ONLY found in level one. ONLY.\n" +
						"\tSFX: This is for special effects, one shots, ect.\n" +
						"\t\tCommon: Sounds that are common between multiple scenes\n" +
						"\t\tLevelOne: Sounds that are only found in level one. ONLY found in level one. ONLY.\n" +
						"Texture Workflow:\n" +
						"\tThis folder is for storing all textures\n" +
						"\tThis folder should be seperated like so:\n" +
						"\t\tCommon: Textures that are common between multiple scenes\n" +
						"\t\tLevelOne: Textures that are only found in level one. ONLY found in level one. ONLY.\n";
		System.IO.File.WriteAllText (Application.dataPath + "/StaticAssets/FolderStructure.txt", staticAssets); 

		AssetDatabase.CreateFolder ("Assets/StaticAssets", "Animations");
		AssetDatabase.CreateFolder ("Assets/StaticAssets/Animations", "Sources");
		
		AssetDatabase.CreateFolder ("Assets/StaticAssets", "AnimatorControllers");
		
		AssetDatabase.CreateFolder ("Assets/StaticAssets", "Effects");
		
		AssetDatabase.CreateFolder ("Assets/StaticAssets", "Models");
		AssetDatabase.CreateFolder ("Assets/StaticAssets/Models", "Character");
		AssetDatabase.CreateFolder ("Assets/StaticAssets/Models", "Enviornment");
		
		AssetDatabase.CreateFolder ("Assets/StaticAssets", "Prefabs");
		AssetDatabase.CreateFolder ("Assets/StaticAssets/Prefabs", "Common");
		
		AssetDatabase.CreateFolder ("Assets/StaticAssets", "Sounds");
		AssetDatabase.CreateFolder ("Assets/StaticAssets/Sounds", "Music");
		AssetDatabase.CreateFolder ("Assets/StaticAssets/Sounds/Music", "Common");
		AssetDatabase.CreateFolder ("Assets/StaticAssets/Sounds", "SFX");
		AssetDatabase.CreateFolder ("Assets/StaticAssets/Sounds/SFX", "Common");
		
		AssetDatabase.CreateFolder ("Assets/StaticAssets", "Textures");
		AssetDatabase.CreateFolder ("Assets/StaticAssets/Textures", "Common");

		AssetDatabase.CreateFolder ("Assets/StaticAssets", "Scenes");

		AssetDatabase.CreateFolder ("Assets", "Testing");
		string testing = "This folder is for everything testing...\n" + 
						 "Any unfinished asset/script/object ect. should be placed in here.\n" +
						 "Once the asset is completed, it should immediately be relocated, renamed, ect. to the appropriate folder.\n" +
						 "Before the final build of the project, this folder should be deleted.";
		System.IO.File.WriteAllText (Application.dataPath + "/Testing/FolderStructure.txt", testing); 

		string entireStructure = " DYNAMIC ASSETS\n " + dynamicAssets + " \n\n\nEXTENSIONS\n" + extensions + " \n\n\nPLUGINS\n" + plugins + " \n\n\nGIZMOS\n" + gizmos + " \n\n\nSCRIPTS\n" + scripts + "\n\n\nSHADERS\n" + shaders + "\n\n\nSTATIC ASSETS\n" + staticAssets + "\n\n\nTESTING\n" + testing;
		System.IO.File.WriteAllText(Application.dataPath + "/FolderStructure.txt" , entireStructure);

		AssetDatabase.Refresh ();
	}
}
