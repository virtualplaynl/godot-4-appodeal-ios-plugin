# (Work In Progress!) Appodeal iOS Plugin for Godot 4
For Godot 3 version see the [original repository](https://github.com/DmitriiFeshchenko/godot-appodeal-ios-plugin)

This repository contains source code of Appodeal **iOS** Plugin for Godot engine.
**If you are looking for the Appodeal plugin itself, follow the first link below.**

## Links:

### [Godot Plugin](https://github.com/virtualplaynl/godot-4-appodeal-editor-plugin)

### [Demo Project](https://github.com/virtualplaynl/godot-4-appodeal-demo-project)

### [Android Plugin](https://github.com/virtualplaynl/godot-4-appodeal-android-plugin)

### [Changelog](CHANGELOG.md)

## Building Instructions:

In case you want to build an iOS plugin yourself (for different Godot Engine or Appodeal SDK versions),
follow these steps:

0. If you have extracted engine headers, you can skip steps 1-3, and manually put them into `/headers/godot` directory.

1. Add the desired version of [Godot Engine](https://github.com/godotengine/godot) to the `/godot` folder.

2. Run `./scripts/generate_headers.sh` in terminal to build the Godot Engine header files.

3. Run `./scripts/extract_headers.sh` to copy all required headers into `/headers/godot` directory.

4. Download Appodeal iOS SDK fat build archive from the [official website](https://docs.appodeal.com/ios/get-started) and copy the header files
from `Appodeal.xcframework` folder into `/headers/appodeal/Appodeal` directory.

5. Run `./scripts/release_xcframework.sh` to build the plugin.

6. Get your plugin from the `/bin/release` directory.
