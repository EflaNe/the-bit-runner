# Bit Runner – Unity C# Scripts 🧬💾

This repository contains the **core C# gameplay scripts** from my 2D endless runner prototype **Bit Runner**, built in **Unity**.

The goal of this repo is to showcase:
- My **C# coding style**
- How I structure **gameplay logic** in Unity
- How I organize **scripts by responsibility**

> 🎮 Note:  
> This is **not** a full Unity project.  
> Only scripts are included here — no scenes, prefabs, art, or other assets.

---

## 📂 What’s Inside?

The repository is organized by responsibility. A typical structure looks like this (you can adjust it to your actual folder layout):

```text
Player/
  PlayerMovementDash.cs
  CameraController.cs
  PlayerDetection.cs
  PlayerMovementDash.cs
  PlayerRendererManager.cs
  PlayerRotater.cs
  SpaceshipTrigger.cs

Data/
  BitMotion.cs
  Enums.cs
  SpaceshipMotionData.cs

Control/
  BitMotionController.cs
  MotionController.cs
  SpaceshipMotionController.cs

General/
  EffectManager.cs
