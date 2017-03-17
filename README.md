### Overview
It is not possible to specify #defines of Effect during the run-time in the MonoGame. As the Effect compilation is performed by the Content Pipeline in the design-time. The MultiCompileEffect tries to solve that. It compiles all possible variants of effect specified by special JSON file. Then the developer chooses the appropriate one during the run-time.

### Usage
1. Reference Myra.Content.Pipeline.dll in the Content Pipeline tool.
![](https://raw.githubusercontent.com/rds1983/Myra/master/Screenshots/ReferenceMyraContentPipeline.png)
2. Change Effect processor to "MultiCompileEffect - MonoGame".
![](https://raw.githubusercontent.com/rds1983/Myra/master/Screenshots/SetMultiCompileEffectProcessor.png)
3. Create variants file. It should reside in the same folder as .fx file and have its name in concatenation with ".variants". I.e. if Effect file name is "MyEffect.fx", then the variants file name is "MyEffect.fx.variants". It's typical contents are:
  ```json
  [
	["LIGHTNING_ON", "LIGHTNING_OFF"],
	["TEXTURING_ON", "TEXTURING_OFF"]
  ]
  ```

  Such variants file will make the processor compile 4 variants with following defines:
  * LIGHTNING_ON;TEXTURING_ON
  * LIGHTNING_OFF;TEXTURING_ON
  * LIGHTNING_ON;TEXTURING_OFF
  * LIGHTNING_OFF;TEXTURING_OFF
4. Compile the effect.
5. In the runtime reference MonoGame.MultiCompileEffect.dll and use the following code:
	```c#
	var mcEffect = Content.Load<MultiCompileEffect>("MyEffect");

	// Variant with 'LIGHTNING_ON' macro defined:
	var effect1 = mcEffect.GetEffect(new[] {"LIGHTNING_ON"}));
	
	// Variant with both 'LIGHTNING_OFF' and 'TEXTURING_ON' macroses defined:
	var effect2 = mcEffect.GetEffect(new[] {"LIGHTNING_OFF", "TEXTURING_ON"});
	```

### License
[https://opensource.org/licenses/MIT](https://opensource.org/licenses/MIT)