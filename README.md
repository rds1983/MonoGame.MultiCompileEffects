### Overview
Multi compile effects library for the Monogame.
Analogue of the Unity3D's multiple shader variants:
http://docs.unity3d.com/Manual/SL-MultipleProgramVariants.html

Right now, it works only for Windows.
### Usage
1. Obtain MonoGame.MultiCompileEffect binaries.
2. Reference MonoGame.MultiCompileEffects.Content.Pipeline.dll in the MonoGame Pipeline:
![Update Reference](/Images/updateReference.png)
3. Update your FXs with the following instructions: 
	```hlsl
	// Adds 4 variants of the effect: with 'ONE', ..., 'FOUR' macros defined accordingly:
	#pragma multi_compile ONE TWO THREE FOUR 

	#ifdef ONE
	...
	#endif

	...
	#ifdef FOUR
	...
	#endif

	// Adds 2 variants of the effect: with 'FEATURE' macro defined and without it:
	#pragma shader_feature FEATURE 
	
	#ifdef FEATURE
	...
	#else
	...
	#endif
	```
4. Set processor of your FXs to "MultiCompileEffect - MonoGame":
![Update Reference](/Images/updateProcessor.png)
5. Run the Pipeline Tool. It'll compile all possible shader variants(8 in our example) in the resulting XNB.
6. In the runtime reference MonoGame.MultiCompileEffect.dll and use the following code:
	```c#
	var mcEffect = Content.Load<MultiCompileEffect>("MyEffect");

	// Default variant: with 'ONE':
	var effect = new Effect(device, mcEffect.GetDefaultEffectCode()); 
	
	// Variant with 'TWO' macro defined:
	var effectTwo = new Effect(device, mcEffect.GetEffectCode(new[] {"TWO"}));
	
	// Variant with 'TWO' and 'FEATURE' macros defined:
	var effectThreeFeature = new Effect(device, mcEffect.GetEffectCode(new[] {"TWO", "FEATURE"}));
	```

