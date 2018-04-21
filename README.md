### Unity Virtual FX about Water Surface
Using Unity 2017.4.1f1

### Flow Map
* Store direction vector in to vertex color.
* Use direction vector to do uv animation, there are 2 uv displacement in different time phase.
* Lerp between two texture color/normal to smooth boundary.

#### Ref
![flowmap](https://raw.githubusercontent.com/douduck08/UnityVFX-WaterSurface/master/img/flowmap.jpg)

* Animating Water Using Flow Maps: http://graphicsrunner.blogspot.tw/2010/08/water-using-flow-maps.html
* Example in slipster216/VertexPaint: https://github.com/slipster216/VertexPaint/tree/master/Examples/FlowMapping

### Water Ripple Effect
![ripple](https://raw.githubusercontent.com/douduck08/UnityVFX-WaterSurface/master/img/ripple.jpg)

[Demo in Youtube](https://www.youtube.com/watch?v=k5ZLzOtziK0)

* Use a script to do raycast and update wave source data
* Compute distance and movement of time，use sin function and normal to make vertex displacement
* For reality, combine some decay into the formula

#### Ref
* keijiro/RippleEffect: https://github.com/keijiro/RippleEffect

### Plugins
* Vertex Painter for Unity: https://github.com/douduck08/VertexPaint