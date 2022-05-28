# GameActions (for Unity)
GameActions is a set of easy to use Unity scripts that can be used to reduce custom scripts that do basic operations like moving objects. It simplifies basic actions and makes them modular in a way that they can be created or modified in the scene without editing a script.

# Getting started
Checkout the Examples to get started. Here's some brief info.

## An action
An action instance does _something_ when its Act or StartActing method gets called.
An action acts only when it's enabled and its object is active.

## Container actions
These are actions that call the Act method of the actions in their level-1 children, all at once (Action Set), one by one in order (Action Sequence),
or one of them randomly (Random Action Set).

## List of provided actions

### Animate\<Property\>Action
Example: `AnimatePositionAction`

These actions animate a property from their current value to the `Target` value in the specified `Duration` (in seconds).
Animations with the `Duration` set to 0 will set the property to the `Target` value immediately.

### DelayAction
Waits for the specified `Duration`. It's useful when we want to put delays in procedures (Sequences).

### ForwardAction
Forwards its action to the specified `Action`.
It can be used to create an action (which can be a set/sequence itself) and share it between multiple actions,
in this case a ForwardAction can be added to each that points to the shared action.

### LoadSceneAction
Loads the specified scene in `SceneName`.

### PlayAudioAction
Plays one or more audio clips on the `Target`'s AudioSource. Just plays the AudioSource if no audio clips are specified.

### PlayParticleSystemAction
Plays the `Target`'s ParticleSystem.

### SetActivationAction
Sets the `Target` object's activation state to the specified `ActivationState`.

### StopGameActionAction
Stops the specified `Action`.

### ToggleActivationAction
Toggles the `Target` object's activation state. It will be false if it's currently true, and true if it's currently false.

## LoopActor
When enabled and its object is active, loops the specified `Action`.

## Making use of actions in a script
This is the main point of GameActions, simplifying actions. These are the steps to do this:

  - Add a field with type `GameAction` in your script, for example `public GameAction ExampleAction`.
  - Call `ExampleAction?.StartActing();` to start it and continue, or `await ExampleAction?.Act();` to wait until the action is done.
  - In the scene, after creating your action (which can be a set/sequence), drag it to the `ExampleAction` field in your script.
