<Query Kind="Program">
  <Connection>
    <ID>7a4e7d40-0157-4052-965f-ef86408a5ee1</ID>
    <Persist>true</Persist>
    <Server>joeymobile</Server>
    <Database>PDAWPF</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

void Main()
{
	
	
	
	int num4 = 0;
	int index = 1;
	int decAgain = 21; //int.Parse("0x15", System.Globalization.NumberStyles.HexNumber);
	
	int noincr = 20;
	int num3 =  (int) Math.Round((double) (noincr + 1f));
	
	float esteel = 2.95E+07f;
	float sbt = 0.689072132f;
	float sbb = 0.417872757f;
	float deltat = 65f;
	float fb = 24438f;
	float sffb = 1.875f;
	float ac = 23.2284832f;
	float sffv = 1.875f;
	float fv = 36.26f;
	float ic = 0.5185544f;
	float gc = 813f;
	float nofasec = 1f;
	float nofasic = 1f;
	float sffas = 2.5f;
	//the following are precal'd values that should appear in the panel design object
	float loadd = 0f;
	float incrld = 5f;
	
	
	float panelwidth = 36f;
	float fastener = 2660f;
	//this should be the eval type
	bool IsWindLoad = true;
	
	float endclip = 804f;
	float sfec = 1.875f;
	
	float interiorclip = 2084f;
	float sfic = 1.875f;
	
	float defrat = 180f;
	
	float spann = 5f;
	
	List<SpanLoadTables> spanLoadOutput = new List<SpanLoadTables>();
	
	for (int i = 0; i < 2; i++)
	{
		
		spanLoadOutput.Add(
			loadspancalcs(
				esteel,
				sbt,
				sbb,
				deltat,
				fb,
				sffb,
				loadd,
				ac,
				sffv,
				fv,
				ic,
				gc,
				defrat,
				endclip,
				interiorclip,
				sfec,
				sfic,
				nofasec,
				nofasic,
				sffas,
				panelwidth,
				fastener,
				false)
			);
			loadd += incrld;
	}
	
	spanLoadOutput.Dump();
	

	
}

public class SpanLoadTables
{
	public float SingleSpanMaximum {get;set;}
	public float SingleSpanMaximumMetric {get;set;}
	public String SingleSpanFactor {get;set;}
	public float DoubleSpanMaximum {get;set;}
	public float DoubleSpanMaximumMetric {get;set;}
	public String DoubleSpanFactor {get;set;}
	public float TripleSpanMaximum {get;set;}
	public float TripleSpanMaximumMetric {get;set;}
	public String TripleSpanFactor {get;set;}
	
}


// Define other methods and classes here

public SpanLoadTables loadspancalcs(
float esteel,
float sbt,
float sbb,
float deltat,
float fb,
float sffb,
float loadd,
float ac,
float sffv,
float fv,
float ic,
float gc,
float defrat,
float endclip,
float interiorclip,
float sfec,
float sfic,
float nofasec,
float nofasic,
float sffas,
//the following are precal'd values that should appear in the panel design object
float panelwidth,
float fastener,
//this should be the eval type
bool IsWindLoad

)
{
/*
	external to this function
	esteel, sbt, sbb, deltat 
	
	internal to this function
	num4, mt
*/
//num4 is internal to this function
    int num4 = 5;
	float Mt = 0f;
	float sb = 0f;
	
	float z = 0.1f;
	float zz;
	float zzz;	
	
	float kk;
	
	float[] c = new float[9];
	String[] Key = new string[9];
	float[] span = new float[10];
	
	//this gets inserted into the final array/design values
	float allowsingle = 0;
	float allowdouble = 0;
	float allowtriple = 0;
	
	string keysingle = string.Empty;
	string keydouble = string.Empty;
	string keytriple = string.Empty;
	

    
	Mt = (float) (((((esteel * 6.5E-06) * sbt) * sbb) * deltat) / ((double) (sbt + sbb)));
    
	
	if (sbb > sbt)
    {
        sb = sbt;
    }
    else
    {
        sb = sbb;
    }
	
    Key[1] = "Bending";
    Key[2] = "End clip";
    Key[3] = "End Fastener";
    Key[4] = "Shear";
    Key[5] = "Deflection";
	
    span[1] = (float) Math.Pow((double) (((8f * fb) * sbb) / ((12f * loadd) * sffb)), 0.5);
    span[2] = 10000f;
    span[3] = 10000f;
    span[4] = ((2f * fv) * ac) / (loadd * sffv);
    
	
	z = 0.1f;
    do
    {
        z += (float) 0.1;
    }
    while (((((double) (((((loadd * z) * z) * z) * z) * 1728f)) / ((76.8 * esteel) * ic)) + ((((12f * loadd) * z) * z) / ((8f * ac) * gc))) <= ((z * 12f) / defrat));
	
    do
    {
        z -= (float) 0.01;
    }
    while (((((double) (((((loadd * z) * z) * z) * z) * 1728f)) / ((76.8 * esteel) * ic)) + ((((12f * loadd) * z) * z) / ((8f * ac) * gc))) >= ((z * 12f) / defrat));
    
	do
    {
        z += (float) 0.001;
    }
    while (((((double) (((((loadd * z) * z) * z) * z) * 1728f)) / ((76.8 * esteel) * ic)) + ((((12f * loadd) * z) * z) / ((8f * ac) * gc))) <= ((z * 12f) / defrat));
	
	Mt.Dump();
    
	span[5] = z - ((float) 0.001);
    
	if (IsWindLoad)
    {
        span[2] = ((2f * endclip) * 12f) / ((loadd * panelwidth) * sfec);
        span[3] = (((2f * nofasec) * fastener) * 12f) / ((loadd * panelwidth) * sffas);
    }
	
    allowsingle = span[1];
    keysingle = Key[1];
    
	int index = 2;
    do
    {
        if (span[index] < allowsingle)
        {
            allowsingle = span[index];
            keysingle = Key[index];
        }
        index++;
        num4 = 5;
    }
    while (index <= num4);
    z = 0.1f;
    do
    {
        z += (float) 0.1;
        kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
        c[1] = -1f / (8f + (24f * kk));
    }
    while ((((((-12f * loadd) * z) * z) * c[1]) / sb) <= (fb / sffb));
    do
    {
        z -= (float) 0.01;
        kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
        c[1] = -1f / (8f + (24f * kk));
    }
    while ((((((-12f * loadd) * z) * z) * c[1]) / sb) >= (fb / sffb));
    do
    {
        z += (float) 0.001;
        kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
        c[1] = -1f / (8f + (24f * kk));
    }
    while ((((((-12f * loadd) * z) * z) * c[1]) / sb) <= (fb / sffb));
    span[1] = z - ((float) 0.001);
    z = 0.1f;
    do
    {
        z += (float) 0.1;
        kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
        c[1] = -1f / (8f + (24f * kk));
        zz = ((float) 0.5) + c[1];
    }
    while (((((((((0.5 + c[1]) * zz) - ((0.5 * zz) * zz)) * loadd) * z) * z) * 12.0) / ((double) sbt)) <= (fb / sffb));
    do
    {
        z -= (float) 0.01;
        kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
        c[1] = -1f / (8f + (24f * kk));
        zz = ((float) 0.5) + c[1];
    }
    while (((((((((0.5 + c[1]) * zz) - ((0.5 * zz) * zz)) * loadd) * z) * z) * 12.0) / ((double) sbt)) >= (fb / sffb));
    do
    {
        z += (float) 0.001;
        kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
        c[1] = -1f / (8f + (24f * kk));
        zz = ((float) 0.5) + c[1];
    }
    while (((((((((0.5 + c[1]) * zz) - ((0.5 * zz) * zz)) * loadd) * z) * z) * 12.0) / ((double) sbt)) <= (fb / sffb));
    span[2] = z - ((float) 0.001);
    span[3] = 10000f;
    span[4] = 10000f;
    span[5] = 10000f;
    span[6] = 10000f;
    z = 0.1f;
    do
    {
        z += (float) 0.1;
        kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
        c[1] = -1f / (8f + (24f * kk));
    }
    while ((((loadd * z) / (2f * ac)) - (((loadd * z) * c[1]) / ac)) <= (fv / sffv));
    do
    {
        z -= (float) 0.01;
        kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
        c[1] = -1f / (8f + (24f * kk));
    }
    while ((((loadd * z) / (2f * ac)) - (((loadd * z) * c[1]) / ac)) >= (fv / sffv));
    do
    {
        z += (float) 0.001;
        kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
        c[1] = -1f / (8f + (24f * kk));
    }
    while ((((loadd * z) / (2f * ac)) - (((loadd * z) * c[1]) / ac)) <= (fv / sffv));
    span[7] = z - ((float) 0.001);
    span[8] = 0f;
    do
    {
        span[8]++;
        kk = (esteel * ic) / ((((144f * ac) * gc) * span[8]) * span[8]);
        c[1] = -1f / (8f + (24f * kk));
        zz = 0f;
        z = 0f;
        do
        {
            zzz = z;
            zz += (float) 0.01;
            z = (float) ((((1728f * loadd) * Math.Pow((double) span[8], 4.0)) / ((double) ((24f * esteel) * ic))) * ((((((1f + (12f * kk)) + (4f * c[1])) * zz) - (((12f * kk) * zz) * zz)) - (((4f * c[1]) + 2f) * Math.Pow((double) zz, 3.0))) + Math.Pow((double) zz, 4.0)));
        }
        while (z >= zzz);
        do
        {
            zzz = z;
            zz -= (float) 0.001;
            z = (float) ((((1728f * loadd) * Math.Pow((double) span[8], 4.0)) / ((double) ((24f * esteel) * ic))) * ((((((1f + (12f * kk)) + (4f * c[1])) * zz) - (((12f * kk) * zz) * zz)) - (((4f * c[1]) + 2f) * Math.Pow((double) zz, 3.0))) + Math.Pow((double) zz, 4.0)));
        }
        while (z >= zzz);
        do
        {
            zzz = z;
            zz += (float) 0.0001;
            z = (float) ((((1728f * loadd) * Math.Pow((double) span[8], 4.0)) / ((double) ((24f * esteel) * ic))) * ((((((1f + (12f * kk)) + (4f * c[1])) * zz) - (((12f * kk) * zz) * zz)) - (((4f * c[1]) + 2f) * Math.Pow((double) zz, 3.0))) + Math.Pow((double) zz, 4.0)));
        }
        while (z >= zzz);
    }
    while (z <= ((span[8] * 12f) / defrat));
    do
    {
        span[8] -= (float) 0.1;
        kk = (esteel * ic) / ((((144f * ac) * gc) * span[8]) * span[8]);
        c[1] = -1f / (8f + (24f * kk));
        zz = 0f;
        z = 0f;
        do
        {
            zzz = z;
            zz += (float) 0.01;
            z = (float) ((((1728f * loadd) * Math.Pow((double) span[8], 4.0)) / ((double) ((24f * esteel) * ic))) * ((((((1f + (12f * kk)) + (4f * c[1])) * zz) - (((12f * kk) * zz) * zz)) - (((4f * c[1]) + 2f) * Math.Pow((double) zz, 3.0))) + Math.Pow((double) zz, 4.0)));
        }
        while (z >= zzz);
        do
        {
            zzz = z;
            zz -= (float) 0.001;
            z = (float) ((((1728f * loadd) * Math.Pow((double) span[8], 4.0)) / ((double) ((24f * esteel) * ic))) * ((((((1f + (12f * kk)) + (4f * c[1])) * zz) - (((12f * kk) * zz) * zz)) - (((4f * c[1]) + 2f) * Math.Pow((double) zz, 3.0))) + Math.Pow((double) zz, 4.0)));
        }
        while (z >= zzz);
        do
        {
            zzz = z;
            zz += (float) 0.0001;
            z = (float) ((((1728f * loadd) * Math.Pow((double) span[8], 4.0)) / ((double) ((24f * esteel) * ic))) * ((((((1f + (12f * kk)) + (4f * c[1])) * zz) - (((12f * kk) * zz) * zz)) - (((4f * c[1]) + 2f) * Math.Pow((double) zz, 3.0))) + Math.Pow((double) zz, 4.0)));
        }
        while (z >= zzz);
    }
    while (z >= ((span[8] * 12f) / defrat));
    do
    {
        span[8] += (float) 0.01;
        kk = (esteel * ic) / ((((144f * ac) * gc) * span[8]) * span[8]);
        c[1] = -1f / (8f + (24f * kk));
        zz = 0f;
        z = 0f;
        do
        {
            zzz = z;
            zz += (float) 0.01;
            z = (float) ((((1728f * loadd) * Math.Pow((double) span[8], 4.0)) / ((double) ((24f * esteel) * ic))) * ((((((1f + (12f * kk)) + (4f * c[1])) * zz) - (((12f * kk) * zz) * zz)) - (((4f * c[1]) + 2f) * Math.Pow((double) zz, 3.0))) + Math.Pow((double) zz, 4.0)));
        }
        while (z >= zzz);
        do
        {
            zzz = z;
            zz -= (float) 0.001;
            z = (float) ((((1728f * loadd) * Math.Pow((double) span[8], 4.0)) / ((double) ((24f * esteel) * ic))) * ((((((1f + (12f * kk)) + (4f * c[1])) * zz) - (((12f * kk) * zz) * zz)) - (((4f * c[1]) + 2f) * Math.Pow((double) zz, 3.0))) + Math.Pow((double) zz, 4.0)));
        }
        while (z >= zzz);
        do
        {
            zzz = z;
            zz += (float) 0.0001;
            z = (float) ((((1728f * loadd) * Math.Pow((double) span[8], 4.0)) / ((double) ((24f * esteel) * ic))) * ((((((1f + (12f * kk)) + (4f * c[1])) * zz) - (((12f * kk) * zz) * zz)) - (((4f * c[1]) + 2f) * Math.Pow((double) zz, 3.0))) + Math.Pow((double) zz, 4.0)));
        }
        while (z >= zzz);
    }
    while (z <= ((span[8] * 12f) / defrat));
    span[8] -= (float) 0.01;
    if (IsWindLoad)
    {
        z = 0.1f;
        do
        {
            z += (float) 0.1;
            kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
            c[1] = -1f / (8f + (24f * kk));
        }
        while (((((loadd * z) * panelwidth) / 24f) + ((((loadd * z) * panelwidth) * c[1]) / 12f)) <= (endclip / sfec));
        do
        {
            z -= (float) 0.01;
            kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
            c[1] = -1f / (8f + (24f * kk));
        }
        while (((((loadd * z) * panelwidth) / 24f) + ((((loadd * z) * panelwidth) * c[1]) / 12f)) >= (endclip / sfec));
        do
        {
            z += (float) 0.001;
            kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
            c[1] = -1f / (8f + (24f * kk));
        }
        while (((((loadd * z) * panelwidth) / 24f) + ((((loadd * z) * panelwidth) * c[1]) / 12f)) <= (endclip / sfec));
        span[3] = z - ((float) 0.001);
        z = 0.1f;
        do
        {
            z += (float) 0.1;
            kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
            c[1] = -1f / (8f + (24f * kk));
        }
        while (((((loadd * z) * panelwidth) / 24f) + ((((loadd * z) * panelwidth) * c[1]) / 12f)) <= ((fastener * nofasec) / sffas));
        do
        {
            z -= (float) 0.01;
            kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
            c[1] = -1f / (8f + (24f * kk));
        }
        while (((((loadd * z) * panelwidth) / 24f) + ((((loadd * z) * panelwidth) * c[1]) / 12f)) >= ((fastener * nofasec) / sffas));
        do
        {
            z += (float) 0.001;
            kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
            c[1] = -1f / (8f + (24f * kk));
        }
        while (((((loadd * z) * panelwidth) / 24f) + ((((loadd * z) * panelwidth) * c[1]) / 12f)) <= ((fastener * nofasec) / sffas));
        span[4] = z - ((float) 0.001);
        z = 0.1f;
        do
        {
            z += (float) 0.1;
            kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
            c[1] = -1f / (8f + (24f * kk));
        }
        while (((((loadd * z) * panelwidth) / 12f) - ((((loadd * z) * panelwidth) * c[1]) / 6f)) <= (interiorclip / sfic));
        do
        {
            z -= (float) 0.01;
            kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
            c[1] = -1f / (8f + (24f * kk));
        }
        while (((((loadd * z) * panelwidth) / 12f) - ((((loadd * z) * panelwidth) * c[1]) / 6f)) >= (interiorclip / sfic));
        do
        {
            z += (float) 0.001;
            kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
            c[1] = -1f / (8f + (24f * kk));
        }
        while (((((loadd * z) * panelwidth) / 12f) - ((((loadd * z) * panelwidth) * c[1]) / 6f)) <= (interiorclip / sfic));
        span[5] = z - ((float) 0.001);
        z = 0.1f;
        do
        {
            z += (float) 0.1;
            kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
            c[1] = -1f / (8f + (24f * kk));
        }
        while (((((loadd * z) * panelwidth) / 12f) - ((((loadd * z) * panelwidth) * c[1]) / 6f)) <= ((fastener * nofasic) / sffas));
        do
        {
            z -= (float) 0.01;
            kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
            c[1] = -1f / (8f + (24f * kk));
        }
        while (((((loadd * z) * panelwidth) / 12f) - ((((loadd * z) * panelwidth) * c[1]) / 6f)) >= ((fastener * nofasic) / sffas));
        do
        {
            z += (float) 0.001;
            kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
            c[1] = -1f / (8f + (24f * kk));
        }
        while (((((loadd * z) * panelwidth) / 12f) - ((((loadd * z) * panelwidth) * c[1]) / 6f)) <= ((fastener * nofasic) / sffas));
        span[6] = z - ((float) 0.001);
    }
    Key[1] = "Bending";
    Key[2] = "Bending";
    Key[3] = "End clip";
    Key[4] = "End Fastener";
    Key[5] = "Int clip";
    Key[6] = "Int Fastener";
    Key[7] = "Shear";
    Key[8] = "Deflection";
    allowdouble = span[1];
    keydouble = Key[1];
    int num2 = 2;
    do
    {
        if (span[num2] < allowdouble)
        {
            allowdouble = span[num2];
            keydouble = Key[num2];
        }
        num2++;
        num4 = 8;
    }
    while (num2 <= num4);
    z = 0.1f;
    do
    {
        z += (float) 0.1;
        kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
        c[1] = -1f / (10f + (12f * kk));
    }
    while ((((((-12f * loadd) * z) * z) * c[1]) / sbt) <= (fb / sffb));
    do
    {
        z -= (float) 0.01;
        kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
        c[1] = -1f / (10f + (12f * kk));
    }
    while ((((((-12f * loadd) * z) * z) * c[1]) / sbt) >= (fb / sffb));
    do
    {
        z += (float) 0.001;
        kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
        c[1] = -1f / (10f + (12f * kk));
    }
    while ((((((-12f * loadd) * z) * z) * c[1]) / sbt) <= (fb / sffb));
    span[1] = z - ((float) 0.001);
    z = 0.1f;
    do
    {
        z += (float) 0.1;
        kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
        c[1] = -1f / (10f + (12f * kk));
        zz = ((float) 0.5) + c[1];
    }
    while (((((((((0.5 + c[1]) * zz) - ((0.5 * zz) * zz)) * loadd) * z) * z) * 12.0) / ((double) sbb)) <= (fb / sffb));
    do
    {
        z -= (float) 0.01;
        kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
        c[1] = -1f / (10f + (12f * kk));
        zz = ((float) 0.5) + c[1];
    }
    while (((((((((0.5 + c[1]) * zz) - ((0.5 * zz) * zz)) * loadd) * z) * z) * 12.0) / ((double) sbb)) >= (fb / sffb));
    do
    {
        z += (float) 0.001;
        kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
        c[1] = -1f / (10f + (12f * kk));
        zz = ((float) 0.5) + c[1];
    }
    while (((((((((0.5 + c[1]) * zz) - ((0.5 * zz) * zz)) * loadd) * z) * z) * 12.0) / ((double) sbb)) <= (fb / sffb));
    span[2] = z - ((float) 0.001);
    span[3] = 10000f;
    span[4] = 10000f;
    span[5] = 10000f;
    span[6] = 10000f;
    z = 0.1f;
    do
    {
        z += (float) 0.1;
        kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
        c[1] = -1f / (10f + (12f * kk));
    }
    while ((((loadd * z) / (2f * ac)) - (((loadd * z) * c[1]) / ac)) <= (fv / sffv));
    do
    {
        z -= (float) 0.01;
        kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
        c[1] = -1f / (10f + (12f * kk));
    }
    while ((((loadd * z) / (2f * ac)) - (((loadd * z) * c[1]) / ac)) >= (fv / sffv));
    do
    {
        z += (float) 0.001;
        kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
        c[1] = -1f / (10f + (12f * kk));
    }
    while ((((loadd * z) / (2f * ac)) - (((loadd * z) * c[1]) / ac)) <= (fv / sffv));
    span[7] = z - ((float) 0.001);
    span[8] = 0f;
    do
    {
        span[8]++;
        kk = (esteel * ic) / ((((144f * ac) * gc) * span[8]) * span[8]);
        c[1] = -1f / (10f + (12f * kk));
        zz = 0f;
        z = 0f;
        do
        {
            zzz = z;
            zz += (float) 0.01;
            z = (float) ((((1728f * loadd) * Math.Pow((double) span[8], 4.0)) / ((double) ((24f * esteel) * ic))) * ((((((1f + (12f * kk)) + (4f * c[1])) * zz) - (((12f * kk) * zz) * zz)) - (((4f * c[1]) + 2f) * Math.Pow((double) zz, 3.0))) + Math.Pow((double) zz, 4.0)));
        }
        while (z >= zzz);
        do
        {
            zzz = z;
            zz -= (float) 0.001;
            z = (float) ((((1728f * loadd) * Math.Pow((double) span[8], 4.0)) / ((double) ((24f * esteel) * ic))) * ((((((1f + (12f * kk)) + (4f * c[1])) * zz) - (((12f * kk) * zz) * zz)) - (((4f * c[1]) + 2f) * Math.Pow((double) zz, 3.0))) + Math.Pow((double) zz, 4.0)));
        }
        while (z >= zzz);
        do
        {
            zzz = z;
            zz += (float) 0.0001;
            z = (float) ((((1728f * loadd) * Math.Pow((double) span[8], 4.0)) / ((double) ((24f * esteel) * ic))) * ((((((1f + (12f * kk)) + (4f * c[1])) * zz) - (((12f * kk) * zz) * zz)) - (((4f * c[1]) + 2f) * Math.Pow((double) zz, 3.0))) + Math.Pow((double) zz, 4.0)));
        }
        while (z >= zzz);
    }
    while (z <= ((span[8] * 12f) / defrat));
    do
    {
        span[8] -= (float) 0.1;
        kk = (esteel * ic) / ((((144f * ac) * gc) * span[8]) * span[8]);
        c[1] = -1f / (10f + (12f * kk));
        zz = 0f;
        z = 0f;
        do
        {
            zzz = z;
            zz += (float) 0.01;
            z = (float) ((((1728f * loadd) * Math.Pow((double) span[8], 4.0)) / ((double) ((24f * esteel) * ic))) * ((((((1f + (12f * kk)) + (4f * c[1])) * zz) - (((12f * kk) * zz) * zz)) - (((4f * c[1]) + 2f) * Math.Pow((double) zz, 3.0))) + Math.Pow((double) zz, 4.0)));
        }
        while (z >= zzz);
        do
        {
            zzz = z;
            zz -= (float) 0.001;
            z = (float) ((((1728f * loadd) * Math.Pow((double) span[8], 4.0)) / ((double) ((24f * esteel) * ic))) * ((((((1f + (12f * kk)) + (4f * c[1])) * zz) - (((12f * kk) * zz) * zz)) - (((4f * c[1]) + 2f) * Math.Pow((double) zz, 3.0))) + Math.Pow((double) zz, 4.0)));
        }
        while (z >= zzz);
        do
        {
            zzz = z;
            zz += (float) 0.0001;
            z = (float) ((((1728f * loadd) * Math.Pow((double) span[8], 4.0)) / ((double) ((24f * esteel) * ic))) * ((((((1f + (12f * kk)) + (4f * c[1])) * zz) - (((12f * kk) * zz) * zz)) - (((4f * c[1]) + 2f) * Math.Pow((double) zz, 3.0))) + Math.Pow((double) zz, 4.0)));
        }
        while (z >= zzz);
    }
    while (z >= ((span[8] * 12f) / defrat));
    do
    {
        span[8] += (float) 0.01;
        kk = (esteel * ic) / ((((144f * ac) * gc) * span[8]) * span[8]);
        c[1] = -1f / (10f + (12f * kk));
        zz = 0f;
        z = 0f;
        do
        {
            zzz = z;
            zz += (float) 0.01;
            z = (float) ((((1728f * loadd) * Math.Pow((double) span[8], 4.0)) / ((double) ((24f * esteel) * ic))) * ((((((1f + (12f * kk)) + (4f * c[1])) * zz) - (((12f * kk) * zz) * zz)) - (((4f * c[1]) + 2f) * Math.Pow((double) zz, 3.0))) + Math.Pow((double) zz, 4.0)));
        }
        while (z >= zzz);
        do
        {
            zzz = z;
            zz -= (float) 0.001;
            z = (float) ((((1728f * loadd) * Math.Pow((double) span[8], 4.0)) / ((double) ((24f * esteel) * ic))) * ((((((1f + (12f * kk)) + (4f * c[1])) * zz) - (((12f * kk) * zz) * zz)) - (((4f * c[1]) + 2f) * Math.Pow((double) zz, 3.0))) + Math.Pow((double) zz, 4.0)));
        }
        while (z >= zzz);
        do
        {
            zzz = z;
            zz += (float) 0.0001;
            z = (float) ((((1728f * loadd) * Math.Pow((double) span[8], 4.0)) / ((double) ((24f * esteel) * ic))) * ((((((1f + (12f * kk)) + (4f * c[1])) * zz) - (((12f * kk) * zz) * zz)) - (((4f * c[1]) + 2f) * Math.Pow((double) zz, 3.0))) + Math.Pow((double) zz, 4.0)));
        }
        while (z >= zzz);
    }
    while (z <= ((span[8] * 12f) / defrat));
    span[8] -= (float) 0.01;
    if (IsWindLoad)
    {
        z = 0.1f;
        do
        {
            z += (float) 0.1;
            kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
            c[1] = -1f / (10f + (12f * kk));
        }
        while (((((loadd * z) * panelwidth) / 24f) + ((((loadd * z) * panelwidth) * c[1]) / 12f)) <= (endclip / sfec));
        do
        {
            z -= (float) 0.01;
            kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
            c[1] = -1f / (10f + (12f * kk));
        }
        while (((((loadd * z) * panelwidth) / 24f) + ((((loadd * z) * panelwidth) * c[1]) / 12f)) >= (endclip / sfec));
        do
        {
            z += (float) 0.001;
            kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
            c[1] = -1f / (10f + (12f * kk));
        }
        while (((((loadd * z) * panelwidth) / 24f) + ((((loadd * z) * panelwidth) * c[1]) / 12f)) <= (endclip / sfec));
        span[3] = z - ((float) 0.001);
        z = 0.1f;
        do
        {
            z += (float) 0.1;
            kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
            c[1] = -1f / (10f + (12f * kk));
        }
        while (((((loadd * z) * panelwidth) / 24f) + ((((loadd * z) * panelwidth) * c[1]) / 12f)) <= ((fastener * nofasec) / sffas));
        do
        {
            z -= (float) 0.01;
            kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
            c[1] = -1f / (10f + (12f * kk));
        }
        while (((((loadd * z) * panelwidth) / 24f) + ((((loadd * z) * panelwidth) * c[1]) / 12f)) >= ((fastener * nofasec) / sffas));
        do
        {
            z += (float) 0.001;
            kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
            c[1] = -1f / (10f + (12f * kk));
        }
        while (((((loadd * z) * panelwidth) / 24f) + ((((loadd * z) * panelwidth) * c[1]) / 12f)) <= ((fastener * nofasec) / sffas));
        span[4] = z - ((float) 0.001);
        z = 0.1f;
        do
        {
            z += (float) 0.1;
            kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
            c[1] = -1f / (10f + (12f * kk));
        }
        while (((((loadd * z) * panelwidth) / 12f) - ((((loadd * z) * panelwidth) * c[1]) / 12f)) <= (interiorclip / sfic));
        do
        {
            z -= (float) 0.01;
            kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
            c[1] = -1f / (10f + (12f * kk));
        }
        while (((((loadd * z) * panelwidth) / 12f) - ((((loadd * z) * panelwidth) * c[1]) / 12f)) >= (interiorclip / sfic));
        do
        {
            z += (float) 0.001;
            kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
            c[1] = -1f / (10f + (12f * kk));
        }
        while (((((loadd * z) * panelwidth) / 12f) - ((((loadd * z) * panelwidth) * c[1]) / 12f)) <= (interiorclip / sfic));
        span[5] = z - ((float) 0.001);
        z = 0.1f;
        do
        {
            z += (float) 0.1;
            kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
            c[1] = -1f / (10f + (12f * kk));
        }
        while (((((loadd * z) * panelwidth) / 12f) - ((((loadd * z) * panelwidth) * c[1]) / 12f)) <= ((fastener * nofasic) / sffas));
        do
        {
            z -= (float) 0.01;
            kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
            c[1] = -1f / (10f + (12f * kk));
        }
        while (((((loadd * z) * panelwidth) / 12f) - ((((loadd * z) * panelwidth) * c[1]) / 12f)) >= ((fastener * nofasic) / sffas));
        do
        {
            z += (float) 0.001;
            kk = (esteel * ic) / ((((144f * ac) * gc) * z) * z);
            c[1] = -1f / (10f + (12f * kk));
        }
        while (((((loadd * z) * panelwidth) / 12f) - ((((loadd * z) * panelwidth) * c[1]) / 12f)) <= ((fastener * nofasic) / sffas));
        span[6] = z - ((float) 0.001);
    }
    Key[1] = "Bending";
    Key[2] = "Bending";
    Key[3] = "End clip";
    Key[4] = "End Fastener";
    Key[5] = "Int clip";
    Key[6] = "Int Fastener";
    Key[7] = "Shear";
    Key[8] = "Deflection";
    allowtriple = span[1];
    keytriple = Key[1];
    int num3 = 2;
    do
    {
        if (span[num3] < allowtriple)
        {
            allowtriple = span[num3];
            keytriple = Key[num3];
        }
        num3++;
        num4 = 8;
    }
    while (num3 <= num4);
	
	/*
		float SingleSpanMaximum {get;set;}
	float SingleSpanMaximumMetric {get;set;}
	String SingleSpanFactor {get;set;}
	float DoubleSpanMaximum {get;set;}
	float DoubleSpanMaximumMetric {get;set;}
	String DoubleSpanFactor {get;set;}
	float TripleSpanMaximum {get;set;}
	float TripleSpanMaximumMetric {get;set;}
	String TripleSpanFactor {get;set;}
	*/
	
	var tempX = new SpanLoadTables{
	SingleSpanMaximum = allowsingle,
	SingleSpanMaximumMetric = (allowsingle * 304.8f),
	SingleSpanFactor = keysingle,
	DoubleSpanMaximum = allowdouble,
	DoubleSpanMaximumMetric = (allowdouble * 304.8f),
	DoubleSpanFactor = keydouble,
	TripleSpanMaximum = allowtriple,
	TripleSpanMaximumMetric = (allowtriple * 304.8f),
	TripleSpanFactor = keytriple};
	
	return tempX;
}

 /*
public void spanloadcalcs()
{
    int num4;
    this.deltat = 0f;
    this.kk = (this.esteel * this.ic) / ((((144f * this.ac) * this.gc) * this.spann) * this.spann);
    this.Mt = (float) (((((this.esteel * 6.5E-06) * this.sbt) * this.sbb) * this.deltat) / ((double) (this.sbt + this.sbb)));
    this.yt = (((this.Mt * this.spann) * this.spann) * 144f) / (2.36E+08f * this.ic);
    if (this.sbb > this.sbt)
    {
        this.sb = this.sbt;
    }
    else
    {
        this.sb = this.sbb;
    }
    this.laod[1] = ((8f * this.fb) * this.sb) / (((this.spann * this.spann) * 12f) * this.sffb);
    this.laod[2] = 100000f;
    this.laod[3] = 100000f;
    this.laod[4] = ((this.fv * 2f) * this.ac) / (this.spann * this.sffv);
    this.laod[5] = (((12f * this.spann) / this.defrat) - this.yt) / (((((((5f * this.spann) * this.spann) * this.spann) * this.spann) * 1728f) / ((384f * this.esteel) * this.ic)) + (((12f * this.spann) * this.spann) / ((8f * this.ac) * this.gc)));
    if (this.RadioButton4.Checked)
    {
        this.laod[2] = (this.endclip * 24f) / ((this.panelwidth * this.spann) * this.sfec);
        this.laod[3] = ((this.fastener * 24f) * this.nofasec) / ((this.panelwidth * this.spann) * this.sffas);
    }
    this.Key[1] = "Bending";
    this.Key[2] = "End clip";
    this.Key[3] = "End Fastener";
    this.Key[4] = "Shear";
    this.Key[5] = "Deflection";
    this.allowsingle = this.laod[1];
    this.keysingle = this.Key[1];
    int index = 2;
    do
    {
        if (this.laod[index] < this.allowsingle)
        {
            this.allowsingle = this.laod[index];
            this.keysingle = this.Key[index];
        }
        index++;
        num4 = 5;
    }
    while (index <= num4);
    this.c[1] = -1f / (8f + (24f * this.kk));
    this.z = ((float) 0.5) + this.c[1];
    this.laod[1] = ((-this.fb * this.sbb) / this.sffb) / (((12f * this.spann) * this.spann) * this.c[1]);
    this.laod[2] = (float) (((double) ((this.sbt * this.fb) / this.sffb)) / ((((((0.5 * this.z) + (this.c[1] * this.z)) - ((0.5 * this.z) * this.z)) * 12.0) * this.spann) * this.spann));
    this.laod[3] = 100000f;
    this.laod[4] = 100000f;
    this.laod[5] = 100000f;
    this.laod[6] = 100000f;
    this.laod[7] = ((((2f * this.fv) * this.ac) * this.spann) / this.sffv) / ((this.spann * this.spann) - (((2f * this.spann) * this.spann) * this.c[1]));
    this.z = 0f;
    do
    {
        this.z += (float) 0.0001;
        this.zz = ((((((((4f * this.z) * this.z) * this.z) - (((12f * this.c[1]) * this.z) * this.z)) - ((6f * this.z) * this.z)) - ((24f * this.kk) * this.z)) + 1f) + (12f * this.kk)) + (4f * this.c[1]);
    }
    while (this.zz >= 0f);
    this.z -= (float) 0.0001;
    this.laod[8] = ((24f * this.esteel) * this.ic) / (((((144f * this.spann) * this.spann) * this.spann) * this.defrat) * ((((((1f + (12f * this.kk)) + (4f * this.c[1])) * this.z) - (((12f * this.kk) * this.z) * this.z)) - (((4f * this.c[1]) + 2f) * ((this.z * this.z) * this.z))) + (((this.z * this.z) * this.z) * this.z)));
    if (this.RadioButton4.Checked)
    {
        this.laod[3] = (((24f * this.endclip) * this.spann) / (this.sfec * this.panelwidth)) / ((this.spann * this.spann) + (((2f * this.spann) * this.spann) * this.c[1]));
        this.laod[4] = (((12f * this.interiorclip) * this.spann) / (this.sfic * this.panelwidth)) / ((this.spann * this.spann) - (((2f * this.spann) * this.spann) * this.c[1]));
        this.laod[5] = ((((24f * this.fastener) * this.nofasec) * this.spann) / (this.sffas * this.panelwidth)) / ((this.spann * this.spann) + (((2f * this.spann) * this.spann) * this.c[1]));
        this.laod[6] = ((((12f * this.fastener) * this.nofasic) * this.spann) / (this.sffas * this.panelwidth)) / ((this.spann * this.spann) - (((2f * this.spann) * this.spann) * this.c[1]));
    }
    this.allowdouble = this.laod[1];
    this.keydouble = this.Key[1];
    this.Key[1] = "Bending";
    this.Key[2] = "Bending";
    this.Key[3] = "End Clip";
    this.Key[4] = "Int clip";
    this.Key[5] = "End Fastener";
    this.Key[6] = "Int Fastener";
    this.Key[7] = "Shear";
    this.Key[8] = "Deflection";
    int num2 = 2;
    do
    {
        if (this.laod[num2] < this.allowdouble)
        {
            this.allowdouble = this.laod[num2];
            this.keydouble = this.Key[num2];
        }
        num2++;
        num4 = 8;
    }
    while (num2 <= num4);
    this.c[1] = -1f / (10f + (12f * this.kk));
    this.z = ((float) 0.5) + this.c[1];
    this.laod[1] = ((-this.fb * this.sbb) / this.sffb) / (((12f * this.spann) * this.spann) * this.c[1]);
    this.laod[2] = (float) (((double) ((this.sbt * this.fb) / this.sffb)) / ((((((0.5 * this.z) + (this.c[1] * this.z)) - ((0.5 * this.z) * this.z)) * 12.0) * this.spann) * this.spann));
    this.laod[3] = 100000f;
    this.laod[4] = 100000f;
    this.laod[5] = 100000f;
    this.laod[6] = 100000f;
    this.laod[7] = ((((2f * this.fv) * this.ac) * this.spann) / this.sffv) / ((this.spann * this.spann) - (((2f * this.spann) * this.spann) * this.c[1]));
    this.z = 0f;
    do
    {
        this.z += (float) 0.0001;
        this.zz = ((((((((4f * this.z) * this.z) * this.z) - (((12f * this.c[1]) * this.z) * this.z)) - ((6f * this.z) * this.z)) - ((24f * this.kk) * this.z)) + 1f) + (12f * this.kk)) + (4f * this.c[1]);
    }
    while (this.zz >= 0f);
    this.z -= (float) 0.0001;
    this.laod[8] = ((24f * this.esteel) * this.ic) / (((((144f * this.spann) * this.spann) * this.spann) * this.defrat) * ((((((1f + (12f * this.kk)) + (4f * this.c[1])) * this.z) - (((12f * this.kk) * this.z) * this.z)) - (((4f * this.c[1]) + 2f) * ((this.z * this.z) * this.z))) + (((this.z * this.z) * this.z) * this.z)));
    if (this.RadioButton4.Checked)
    {
        this.laod[3] = (((24f * this.endclip) * this.spann) / (this.sfec * this.panelwidth)) / ((this.spann * this.spann) + (((2f * this.spann) * this.spann) * this.c[1]));
        this.laod[4] = (((12f * this.interiorclip) * this.spann) / (this.sfic * this.panelwidth)) / ((this.spann * this.spann) - ((this.spann * this.spann) * this.c[1]));
        this.laod[5] = ((((24f * this.fastener) * this.nofasec) * this.spann) / (this.sffas * this.panelwidth)) / ((this.spann * this.spann) + (((2f * this.spann) * this.spann) * this.c[1]));
        this.laod[6] = ((((12f * this.fastener) * this.nofasic) * this.spann) / (this.sffas * this.panelwidth)) / ((this.spann * this.spann) - ((this.spann * this.spann) * this.c[1]));
    }
    this.allowtriple = this.laod[1];
    this.keytriple = this.Key[1];
    int num3 = 2;
    do
    {
        if (this.laod[num3] < this.allowtriple)
        {
            this.allowtriple = this.laod[num3];
            this.keytriple = this.Key[num3];
        }
        num3++;
        num4 = 8;
    }
    while (num3 <= num4);
}

 

 

 

*/