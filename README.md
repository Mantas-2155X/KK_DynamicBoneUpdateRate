# KK_DynamicBoneUpdateRate

This plugin allows you to change the default refresh rate (60) of the Dynamic Bones.
If you have more than 60fps you will experience certain dynamic bones like skirt or breasts jittering rapidly.
Using this plugin you can set the refresh rate higher to avoid the jitter.

**Performance** 
Scenario: Default home map, one female character, male hidden.

**Test 1**
Plugin disabled, using default (60) refresh rate.
*CPU: ~35%*
*GPU: ~75%*

**Test 2**
Plugin enabled, refresh rate set to the maximum value (240).
*CPU: ~38%*
*GPU: ~75%*

As seen by the tests, the plugin should not affect performance with one character.
