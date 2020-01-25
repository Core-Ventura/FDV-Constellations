# FDV-Constellations

Constellations es un juego de exploración en el que descubriremos las estrellas y las constelaciones que conforman nuestro mapa estelar. El objetivo del proyecto es gamificar la experiencia de aprendizaje de nuestra bóveda celeste.

![](Gif-Constellations.gif)

En primer lugar el observatorio nos pide encontrar todas las estrellas de una constelación concreta. A través de la cámara y al hacer zoom enfocando una estrella la descubriremos y nos aparecerá su panel de información así como su nombre en el mapa.

![](Screenshot_1.PNG)

Una vez descubiertas todas las estrellas de la constelación, la desbloquearemos y obtendremos sus "líneas", pasando así a la siguiente constelación objetivo.

![](Screenshot_2.PNG)

Ejemplo del mapa celeste con todas las constelaciones - Se trata de un pseudo-skybox con un mapa de constelaciones obtenido de una panorámica de gran calidad de la NASA.

![](Screenshot_3.PNG)

Además de estar escaladas según su magnitud aparente, las estrellas brillan con el color correspondiente a su temperatura superficial, siguiendo una escala de categorías de color ajustada a la visibilidad del ojo humano.

![](Screenshot_4.PNG)

Para las estrellas de fondo, he utilizado diversos sistema de partículas con materiales con color emisivo HDR. Además, he añadido a la interfaz diversos botones para gestionar la visibilidad de los elementos que podemos ver en el cielo (nombres de estrellas, nombres de constelaciones, líneas de constelaciones, y a modo de demostración, todas las constelaciones).
 
![](Screenshot_5.PNG)

---

**Realizando el prototipo...**

El proyecto nació como un juego en 2D, en el que se exploraría el espacio en busca de estrellas y constelaciones, no obstante, al montar el mapa estelar en una pseudo-skybox, fue aparente que resultaría mucho más natural y llamativo continuar con un planteamiento tridimensional, pero con una jugabilidad bastante bidimensional. Además, de este modo podría probar finalmente tanto el nuevo sistema de materiales gráfico de Unity *Shader Graph* y de efectos visuales *Visual Effects Graph*.


**Demasiadas estrellas**

El primer problema a resolver era como gestionar una arquitectura que permitiera un número elevado de estrellas y toda su información. Con prefabs resultaría muy costoso y muy complicado de añadir y modificar estrellas, así que me decanté por utilizar *Scriptable Objects*, de este modo generé dos clases, **Star**(que hereda de Scriptable Objects) y **StarDisplay**(que se encargará de obtener la información del scriptable object Star y mostrarla).

De este modo, tendremos una especie de base de datos en la que cada estrella tendrá asociada su scriptable object, el cual contiene toda su información. En principió generé manualmente las estrellas, pero de cara a una posible ampliación del proyecto, se podría crear fácilmente un script que generara todo los objetos a partir de un CSV.


**Tamaño, color y brillo**

Para el brillo pensé inicialmente en utilizar la luminosidad de cada estrella y la intensidad de componentes de tipo *light*, pero en realidad, ya tenemos una medida del brillo de la estrella en la denominada magnitud aparente de la misma. Además, si utilizaba un material emisivo HDR del nuevo pipeline universal de Unity no habría necesidad de usar componentes de luz. Del mismo modo que el brillo, decidí escalar las estrellas en función de su magnitud aparente. Para los colores si que utilicé directamente la temperatura de las estrellas.

Para generar su modelo, utilicé el *Visual Effects Graph*, para generar una esfera de partículas emisivas (material HDR) atraídas y repelidas por un campo de fuerza vectorial, dando el efecto de llamaradas solares.


**¿Como se descrubre una estrella?**

Tenemos una clase que controla el movimiento de la cámara, y que, dependiendo de nuestro input del ratón y teclado hará *zoom* a través de la modificación del FOV de la cámara. De este modo, condicionado por el FOV, emitiremos un raycast desde la posición inicial de nuestro observatorio en dirección de su vector forward. Al impactar con una estrella, se comprobará si ya está descubierta y se mostrará su panel de información. Del mismo modo, se realizarán comprobaciones para saber si tenemos descubiertas todas las estrellas de una constelación y, de ser así, activar las líneas de la constelación correspondiente.


**Audio dinámico**

La clase **Audio Manager** se encarga de gestionar los audio sources de ambiente y efectos de sonido. A través del código, se realizarán transiciones de volumen entre los sources para pasar de una música ambiente de montaña a un sonido más espacial y focalizador en función del nivel de zoom en el que nos encontremos. 


**La interfaz**

La interfaz se compone del tres secciones o paneles:
1) Abajo a la izquierda encontramos, dibujado a mano en Krita, el observatorio (cuya lente se escala en función del nivel de zoom de la cámara), unas montañas y demás vegetación diversa. En este panel encontraremos los botones de control de visibilidad.
2) Arriba y al centro encontramos el panel de desafío, donde se nos muestra la constelación a buscar y la situación actual de planetas descubiertos.
3) Finalmente, a la derecha encontraremos el panel de información de la estrella, donde se nos mostrará toda la información del cuerpo celeste.
