<Query Kind="Program" />

void Main()
{
	var WindLoad = new List<KeyValuePair<String,Float>>	
	{
		new KeyValuePair<String,Float>("Bending",  ((8f*fb)*sb)/(((spann*spann)*12f)*sffb)),
		new KeyValuePair<String,Float>("End clip", 100000f),
		new KeyValuePair<String,Float>("End Fastener", 100000f),
		new KeyValuePair<String,Float>("Shear", ((fv*2f)*ac)/(spann*sffv)),
		new KeyValuePair<String,Float>("Deflection", (((12f*spann)/defrat) - yt)/(((((((5f*spann)*spann)*spann)*spann)*1728f)/((384f*esteel)*ic)) + (((12f*spann)*spann)/((8f*ac)*gc))))
	};
	
	var LiveLoad = new List<KeyValuePair<String,Float>>	
	{
		new KeyValuePair<String,Float>("Bending", (float)((-fb*sbb)/sffb)/(((12f*spann)*spann)*c[1])),
		new KeyValuePair<String,Float>("Bending", (float) ((sbt*fb)/sffb/((((((0.5*z) + (c[1]*z)) - ((0.5*z)*z))*12.0)*spann)*spann))),
		new KeyValuePair<String,Float>("End clip", 100000f),
		new KeyValuePair<String,Float>("Int clip", 100000f),
		new KeyValuePair<String,Float>("End Fastener", 100000f),
		new KeyValuePair<String,Float>("Int Fastener", 100000f),
		new KeyValuePair<String,Float>("Shear", ((((2f*fv)*ac)*spann)/sffv)/((spann*spann) - (((2f*spann)*spann)*c[1]))),
		new KeyValuePair<String,Float>("Deflection", ((24f*esteel)*ic)/
                      (((((144f*spann)*spann)*spann)*defrat)*
                       ((((((1f + (12f*kk)) + (4f*c[1]))*z) - (((12f*kk)*z)*z)) - (((4f*c[1]) + 2f)*((z*z)*z))) +
                        (((z*z)*z)*z))))
	};
}

// Define other methods and classes here
