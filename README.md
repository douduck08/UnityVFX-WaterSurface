### Unity Virtual FX about Water Surface

### Flow Map
* Store direction vector in to vertex color.
* Use direction vector to do uv animation, there are 2 uv displacement in different time phase.
* Lerp between two texture color/normal to smooth boundary.

#### Ref
![flowmap](https://github.com/douduck08/repository/UnityVFX-WaterSurface/master/img/flowmap.jpg)

* Animating Water Using Flow Maps: http://graphicsrunner.blogspot.tw/2010/08/water-using-flow-maps.html
* Example in slipster216/VertexPaint: https://github.com/slipster216/VertexPaint/tree/master/Examples/FlowMapping

### Water Surface Wave
Incomplete

#### Ref
* ya7gisa0/Unity-Wave-Propagation-Water-Ripple: https://github.com/ya7gisa0/Unity-Wave-Propagation-Water-Ripple
* unity3d-jp/WaveShooter: https://github.com/unity3d-jp/WaveShooter
* 【Unite 2017 Tokyo】スマートフォンでどこまでできる？: https://www.slideshare.net/UnityTechnologiesJapan/unite-2017-tokyo3d-76689196

### Water Ripple Effect
![ripple](https://github.com/douduck08/repository/UnityVFX-WaterSurface/master/img/ripple.jpg)

* Use a script to do raycast and update wave source data
* Compute distance and movement of time，use sin function and normal to make vertex displacement
* For reality, combine some decay into the formula
[Demo in Youtube](https://www.youtube.com/watch?v=k5ZLzOtziK0)

#### Ref
* keijiro/RippleEffect: https://github.com/keijiro/RippleEffect

### Plugins
* Vertex Painter for Unity: https://github.com/douduck08/VertexPaint