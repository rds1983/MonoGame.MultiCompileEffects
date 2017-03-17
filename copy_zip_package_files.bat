rem delete existing
rmdir "ZipPackage" /Q /S

rem Create required folders
mkdir "ZipPackage"

set "CONFIGURATION=Release"

rem Copy output files
copy "MonoGame.MultiCompileEffects\bin\%CONFIGURATION%\MonoGame.MultiCompileEffects.dll" ZipPackage /Y
copy "MonoGame.MultiCompileEffects\bin\%CONFIGURATION%\MonoGame.MultiCompileEffects.pdb" ZipPackage /Y
copy "MonoGame.MultiCompileEffects.Content.Pipeline\bin\%CONFIGURATION%\MonoGame.MultiCompileEffects.Content.Pipeline.dll" ZipPackage /Y
copy "MonoGame.MultiCompileEffects.Content.Pipeline\bin\%CONFIGURATION%\MonoGame.MultiCompileEffects.Content.Pipeline.pdb" ZipPackage /Y

