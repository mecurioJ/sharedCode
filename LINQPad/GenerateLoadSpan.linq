<Query Kind="Program" />

void Main()
{
	GenerateLoadSpan(65.0f,0f,1524.0f,5f);	
}

public class calcout
{
	public float[] spans {get;set;}
	public float allowsingle {get;set;}
	public float allowdouble {get;set;}
	public float allowtriple {get;set;}
	public string keysingle {get;set;}
	public string keydouble {get;set;}
	public string keytriple {get;set;}
	public float spann {get;set;}
	
}

private void GenerateLoadSpan(
	float deltat, 
	float spann,
	float beginspanm,
	float beginspan
	)
        {
			//var spann = 0.0f;
//			var visiList = new IEnumerable<Object>();		
//			var visiTuple =  new Tuple<bool,bool,bool,bool,bool,bool,bool>();
            deltat = 0f;
            if (false)
            {
                //Interaction.MsgBox("Tables not available with thermal loads.", MsgBoxStyle.ApplicationModal, null);
            }
            else
            {
                int num4;
                int index = 1;
                do
                {
					var visiTuple = System.Tuple.Create(false,false,false,false,false,false,false);
//					visiList.Add(visiTuple);
////                    l0[index].Visible = false;
////                    l1[index].Visible = false;
////                    l2[index].Visible = false;
////                    l3[index].Visible = false;
////                    l4[index].Visible = false;
////                    l5[index].Visible = false;
////                    l6[index].Visible = false;
                    index++;
                    num4 = 0x15;
                }
				
                while (index <= num4);
				if (false)
                {
                    spann = (float)(((double)beginspanm) / 304.8);
                }
                else
                {
                    spann = beginspan;
                }
//                Label51.Text = "Span";
//                Label45.Visible = true;
//                Label46.Visible = true;
//                Label47.Visible = true;
//                Label49.Visible = true;
//                Label50.Visible = true;
//                Label51.Visible = true;
//                Label52.Visible = true;
//                Label53.Visible = true;
//                Label54.Visible = true;
//                Label55.Visible = true;
                //noincr = Conversions.ToSingle(TextBox4.Text);
                var noincr = 20;
                int num3 = (int)Math.Round((double)(noincr + 1f));
                for (int i = 1; i <= num3; i++)
                {
				var ttx = 
                    loadspancalcs(
						24438.0f,
						2.95E+7f,
						0.689072132f,
						0.417872757f,
						65.0f,
						20.0f,
						1.875f,
						0.0f,
						36.26f,
						23.2284832f,
						1.875f,
						0.5185544f,
						813.0f,
						180.0f,
						804.0f,
						36.0f,
						1.875f,
						1.0f,
						2660.0f,
						2.5f,
						2084.0f,
						1.875f,
						1.0f,
						spann);
						
						//ttx.Dump();
				var tempLoadSpanCalcItem = new LoadSpanCalcItem();
                    if (true)
                    {
                        if ((spann - (int)spann) <= 0.95833333333333337)
                        {
//                            l0[i].Text = 
//							Conversions.ToString(Conversion.Int(spann)) + 
//							"' - " + 
//							Strings.Format((spann - Conversion.Int(spann)) * 12f, "#0") + 
//							"''";
							tempLoadSpanCalcItem.Length = String.Format(@"{0} ' - {1}""",
							spann,
							(spann - (int)(spann)) * 12f
							);
                        }
                        else
                        {
//                            l0[i].Text = 
//								Conversions.ToString((float)(Conversion.Int(spann) + 1f)) + "' - 0''";
							tempLoadSpanCalcItem.Length = String.Format(@"{0} ' - {1}""",
							(int)spann,
							"0"
							);
                        }
                    }
                    else if (false)
                    {
						tempLoadSpanCalcItem.Length = spann.ToString("#.00");
                        //l0[i].Text = Strings.Format(spann, "#.00");
                    }
                    else if (false)
                    {
                        tempLoadSpanCalcItem.Length = (spann * 304.8).ToString("#.");
						//l0[i].Text = Strings.Format(spann * 304.8, "#.");
                    }
                    if (false)
                    {
						tempLoadSpanCalcItem.SingleMaximum = ttx.allowsingle * 0.04788;
                        //l1[i].Text = Strings.Format(allowsingle * 0.04788, "0.000");
                    }
                    else
                    {
						tempLoadSpanCalcItem.SingleMaximum = ttx.allowsingle;
                        //l1[i].Text = Strings.Format(allowsingle, "#.00");
                    }
						tempLoadSpanCalcItem.SingleFactor = ttx.keysingle;
                    //l2[i].Text = keysingle;
                    if (false)
                    {
						tempLoadSpanCalcItem.DoubleMaximum = ttx.allowdouble * 0.04788;
                        //l3[i].Text = Strings.Format(allowdouble * 0.04788, "0.000");
                    }
                    else
                    {
						tempLoadSpanCalcItem.DoubleMaximum = ttx.allowdouble;
                        //l3[i].Text = Strings.Format(allowdouble, "#.00");
                    }
						tempLoadSpanCalcItem.DoubleFactor = ttx.keydouble;
                    //l4[i].Text = keydouble;
                    if (false)
                    {
						tempLoadSpanCalcItem.TripleMaximum = ttx.allowtriple * 0.04788;
                        //l5[i].Text = Strings.Format(allowtriple * 0.04788, "0.000");
                    }
                    else
                    {
						tempLoadSpanCalcItem.TripleMaximum = ttx.allowtriple;
                        //l5[i].Text = Strings.Format(allowtriple, "#.00");
                    }
					tempLoadSpanCalcItem.TripleFactor = ttx.keytriple;
                    //l6[i].Text = keytriple;
					
					float incrsp = 3.0f;
					float incrspm = 1.0f;
                    if (false)
                    {
                        spann += (float)((((double)incrspm) / 25.4) / 12.0);
                    }
                    else
                    {
                        spann += incrsp / 12f;
                    }
//                    l0[i].Visible = true;
//                    l1[i].Visible = true;
//                    l2[i].Visible = true;
//                    l3[i].Visible = true;
//                    l4[i].Visible = true;
//                    l5[i].Visible = true;
//                    l6[i].Visible = true;
tempLoadSpanCalcItem.Dump();
                }
//                CheckBox9.Checked = true;
//                Panel3.Height = 0x260;

            }
        }

public class LoadSpanCalcItem
		{
			public String Length {get;set;}
			public double SingleMaximum {get;set;}
			public String SingleFactor {get;set;}
			public double DoubleMaximum  {get;set;}
			public String DoubleFactor  {get;set;}
			public double TripleMaximum  {get;set;}
			public String TripleFactor {get; set;}
			
		}

 public calcout loadspancalcs(
            float fb, float esteel, float sbt, float sbb, float deltat, float loadd, float sffb,
            float sb, float fv, float ac, float sffv, float ic, float gc, float defrat,
            float endclip, float panelwidth, float sfec, float nofasec, float fastener,
            float sffas, float interiorclip, float sfic, float nofasic, float spann
            )
        {
			calcout retVal = new calcout();
            string[] Key = new string[26];
            float[] span = new float[10];

			var load = true;

            int num4;

            float Mt = (float)(((((esteel * 6.5E-06) * sbt) * sbb) * deltat) / ((double)(sbt + sbb)));
            float z = 0.1f;
            float kk = 0.0f;
            float zz = 0.0f;
            float zzz = 0.0f;


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
            span[1] = (float) Math.Pow(((8f*fb)*sbb)/((12f*loadd)*sffb), 0.5);
            span[2] = 10000f;
            span[3] = 10000f;
            span[4] = ((2f*fv)*ac)/(loadd*sffv);

            do
            {
                z += (float) 0.1;
            } while (((((((loadd*z)*z)*z)*z)*1728f/((76.8*esteel)*ic)) + ((((12f*loadd)*z)*z)/((8f*ac)*gc))) <=
                     ((z*12f)/defrat));
            do
            {
                z -= (float) 0.01;
            } while (((((((loadd*z)*z)*z)*z)*1728f/((76.8*esteel)*ic)) + ((((12f*loadd)*z)*z)/((8f*ac)*gc))) >=
                     ((z*12f)/defrat));
            do
            {
                z += (float) 0.001;
            } while (((((((loadd*z)*z)*z)*z)*1728f/((76.8*esteel)*ic)) + ((((12f*loadd)*z)*z)/((8f*ac)*gc))) <=
                     ((z*12f)/defrat));
            span[5] = z - ((float) 0.001);
            
            //if (RadioButton4.Checked)
            //if(App.PanelAttribs.LoadType.EvalType.Equals(LoadTypeEval.Wind))
			if(load)
            {
                span[2] = ((2f*endclip)*12f)/((loadd*panelwidth)*sfec);
                span[3] = (((2f*nofasec)*fastener)*12f)/((loadd*panelwidth)*sffas);
            }
            var allowsingle = span[1];
            var keysingle = Key[1];
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
            } while (index <= num4);
            z = 0.1f;
            float[] c = new float[10];
            do
            {
                z += (float) 0.1;
                kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                c[1] = -1f/(8f + (24f*kk));
            } while ((((((-12f*loadd)*z)*z)*c[1])/sb) <= (fb/sffb));
            do
            {
                z -= (float) 0.01;
                kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                c[1] = -1f/(8f + (24f*kk));
            } while ((((((-12f*loadd)*z)*z)*c[1])/sb) >= (fb/sffb));
            do
            {
                z += (float) 0.001;
                kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                c[1] = -1f/(8f + (24f*kk));
            } while ((((((-12f*loadd)*z)*z)*c[1])/sb) <= (fb/sffb));
            span[1] = z - ((float) 0.001);
            z = 0.1f;
            
            do
            {
                z += (float) 0.1;
                kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                c[1] = -1f/(8f + (24f*kk));
                zz = ((float) 0.5) + c[1];
            } while (((((((((0.5 + c[1])*zz) - ((0.5*zz)*zz))*loadd)*z)*z)*12.0)/((double) sbt)) <= (fb/sffb));
            do
            {
                z -= (float) 0.01;
                kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                c[1] = -1f/(8f + (24f*kk));
                zz = ((float) 0.5) + c[1];
            } while (((((((((0.5 + c[1])*zz) - ((0.5*zz)*zz))*loadd)*z)*z)*12.0)/((double) sbt)) >= (fb/sffb));
            do
            {
                z += (float) 0.001;
                kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                c[1] = -1f/(8f + (24f*kk));
                zz = ((float) 0.5) + c[1];
            } while (((((((((0.5 + c[1])*zz) - ((0.5*zz)*zz))*loadd)*z)*z)*12.0)/((double) sbt)) <= (fb/sffb));
            span[2] = z - ((float) 0.001);
            span[3] = 10000f;
            span[4] = 10000f;
            span[5] = 10000f;
            span[6] = 10000f;
            z = 0.1f;
            do
            {
                z += (float) 0.1;
                kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                c[1] = -1f/(8f + (24f*kk));
            } while ((((loadd*z)/(2f*ac)) - (((loadd*z)*c[1])/ac)) <= (fv/sffv));
            do
            {
                z -= (float) 0.01;
                kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                c[1] = -1f/(8f + (24f*kk));
            } while ((((loadd*z)/(2f*ac)) - (((loadd*z)*c[1])/ac)) >= (fv/sffv));
            do
            {
                z += (float) 0.001;
                kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                c[1] = -1f/(8f + (24f*kk));
            } while ((((loadd*z)/(2f*ac)) - (((loadd*z)*c[1])/ac)) <= (fv/sffv));
            span[7] = z - ((float) 0.001);
            span[8] = 0f;
            do
            {
                span[8]++;
                kk = (esteel*ic)/((((144f*ac)*gc)*span[8])*span[8]);
                c[1] = -1f/(8f + (24f*kk));
                zz = 0f;
                z = 0f;
                do
                {
                    zzz = z;
                    zz += (float) 0.01;
                    z =
                        (float)
                            ((((1728f*loadd)*Math.Pow((double) span[8], 4.0))/((double) ((24f*esteel)*ic)))*
                             ((((((1f + (12f*kk)) + (4f*c[1]))*zz) - (((12f*kk)*zz)*zz)) -
                               (((4f*c[1]) + 2f)*Math.Pow((double) zz, 3.0))) + Math.Pow((double) zz, 4.0)));
                } while (z >= zzz);
                do
                {
                    zzz = z;
                    zz -= (float) 0.001;
                    z =
                        (float)
                            ((((1728f*loadd)*Math.Pow((double) span[8], 4.0))/((double) ((24f*esteel)*ic)))*
                             ((((((1f + (12f*kk)) + (4f*c[1]))*zz) - (((12f*kk)*zz)*zz)) -
                               (((4f*c[1]) + 2f)*Math.Pow((double) zz, 3.0))) + Math.Pow((double) zz, 4.0)));
                } while (z >= zzz);
                do
                {
                    zzz = z;
                    zz += (float) 0.0001;
                    z =
                        (float)
                            ((((1728f*loadd)*Math.Pow((double) span[8], 4.0))/((double) ((24f*esteel)*ic)))*
                             ((((((1f + (12f*kk)) + (4f*c[1]))*zz) - (((12f*kk)*zz)*zz)) -
                               (((4f*c[1]) + 2f)*Math.Pow((double) zz, 3.0))) + Math.Pow((double) zz, 4.0)));
                } while (z >= zzz);
            } while (z <= ((span[8]*12f)/defrat));
            do
            {
                span[8] -= (float) 0.1;
                kk = (esteel*ic)/((((144f*ac)*gc)*span[8])*span[8]);
                c[1] = -1f/(8f + (24f*kk));
                zz = 0f;
                z = 0f;
                do
                {
                    zzz = z;
                    zz += (float) 0.01;
                    z =
                        (float)
                            ((((1728f*loadd)*Math.Pow((double) span[8], 4.0))/((double) ((24f*esteel)*ic)))*
                             ((((((1f + (12f*kk)) + (4f*c[1]))*zz) - (((12f*kk)*zz)*zz)) -
                               (((4f*c[1]) + 2f)*Math.Pow((double) zz, 3.0))) + Math.Pow((double) zz, 4.0)));
                } while (z >= zzz);
                do
                {
                    zzz = z;
                    zz -= (float) 0.001;
                    z =
                        (float)
                            ((((1728f*loadd)*Math.Pow((double) span[8], 4.0))/((double) ((24f*esteel)*ic)))*
                             ((((((1f + (12f*kk)) + (4f*c[1]))*zz) - (((12f*kk)*zz)*zz)) -
                               (((4f*c[1]) + 2f)*Math.Pow((double) zz, 3.0))) + Math.Pow((double) zz, 4.0)));
                } while (z >= zzz);
                do
                {
                    zzz = z;
                    zz += (float) 0.0001;
                    z =
                        (float)
                            ((((1728f*loadd)*Math.Pow((double) span[8], 4.0))/((double) ((24f*esteel)*ic)))*
                             ((((((1f + (12f*kk)) + (4f*c[1]))*zz) - (((12f*kk)*zz)*zz)) -
                               (((4f*c[1]) + 2f)*Math.Pow((double) zz, 3.0))) + Math.Pow((double) zz, 4.0)));
                } while (z >= zzz);
            } while (z >= ((span[8]*12f)/defrat));
            do
            {
                span[8] += (float) 0.01;
                kk = (esteel*ic)/((((144f*ac)*gc)*span[8])*span[8]);
                c[1] = -1f/(8f + (24f*kk));
                zz = 0f;
                z = 0f;
                do
                {
                    zzz = z;
                    zz += (float) 0.01;
                    z =
                        (float)
                            ((((1728f*loadd)*Math.Pow((double) span[8], 4.0))/((double) ((24f*esteel)*ic)))*
                             ((((((1f + (12f*kk)) + (4f*c[1]))*zz) - (((12f*kk)*zz)*zz)) -
                               (((4f*c[1]) + 2f)*Math.Pow((double) zz, 3.0))) + Math.Pow((double) zz, 4.0)));
                } while (z >= zzz);
                do
                {
                    zzz = z;
                    zz -= (float) 0.001;
                    z =
                        (float)
                            ((((1728f*loadd)*Math.Pow((double) span[8], 4.0))/((double) ((24f*esteel)*ic)))*
                             ((((((1f + (12f*kk)) + (4f*c[1]))*zz) - (((12f*kk)*zz)*zz)) -
                               (((4f*c[1]) + 2f)*Math.Pow((double) zz, 3.0))) + Math.Pow((double) zz, 4.0)));
                } while (z >= zzz);
                do
                {
                    zzz = z;
                    zz += (float) 0.0001;
                    z =
                        (float)
                            ((((1728f*loadd)*Math.Pow((double) span[8], 4.0))/((double) ((24f*esteel)*ic)))*
                             ((((((1f + (12f*kk)) + (4f*c[1]))*zz) - (((12f*kk)*zz)*zz)) -
                               (((4f*c[1]) + 2f)*Math.Pow((double) zz, 3.0))) + Math.Pow((double) zz, 4.0)));
                } while (z >= zzz);
            } while (z <= ((span[8]*12f)/defrat));
            span[8] -= (float) 0.01;
            
            //if (RadioButton4.Checked)
            //if (App.PanelAttribs.LoadType.EvalType.Equals(LoadTypeEval.Wind))
			if(load)
            {
                z = 0.1f;
                do
                {
                    z += (float) 0.1;
                    kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                    c[1] = -1f/(8f + (24f*kk));
                } while (((((loadd*z)*panelwidth)/24f) + ((((loadd*z)*panelwidth)*c[1])/12f)) <= (endclip/sfec));
                do
                {
                    z -= (float) 0.01;
                    kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                    c[1] = -1f/(8f + (24f*kk));
                } while (((((loadd*z)*panelwidth)/24f) + ((((loadd*z)*panelwidth)*c[1])/12f)) >= (endclip/sfec));
                do
                {
                    z += (float) 0.001;
                    kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                    c[1] = -1f/(8f + (24f*kk));
                } while (((((loadd*z)*panelwidth)/24f) + ((((loadd*z)*panelwidth)*c[1])/12f)) <= (endclip/sfec));
                span[3] = z - ((float) 0.001);
                z = 0.1f;
                do
                {
                    z += (float) 0.1;
                    kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                    c[1] = -1f/(8f + (24f*kk));
                } while (((((loadd*z)*panelwidth)/24f) + ((((loadd*z)*panelwidth)*c[1])/12f)) <=
                         ((fastener*nofasec)/sffas));
                do
                {
                    z -= (float) 0.01;
                    kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                    c[1] = -1f/(8f + (24f*kk));
                } while (((((loadd*z)*panelwidth)/24f) + ((((loadd*z)*panelwidth)*c[1])/12f)) >=
                         ((fastener*nofasec)/sffas));
                do
                {
                    z += (float) 0.001;
                    kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                    c[1] = -1f/(8f + (24f*kk));
                } while (((((loadd*z)*panelwidth)/24f) + ((((loadd*z)*panelwidth)*c[1])/12f)) <=
                         ((fastener*nofasec)/sffas));
                span[4] = z - ((float) 0.001);
                z = 0.1f;
                do
                {
                    z += (float) 0.1;
                    kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                    c[1] = -1f/(8f + (24f*kk));
                } while (((((loadd*z)*panelwidth)/12f) - ((((loadd*z)*panelwidth)*c[1])/6f)) <= (interiorclip/sfic));
                do
                {
                    z -= (float) 0.01;
                    kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                    c[1] = -1f/(8f + (24f*kk));
                } while (((((loadd*z)*panelwidth)/12f) - ((((loadd*z)*panelwidth)*c[1])/6f)) >= (interiorclip/sfic));
                do
                {
                    z += (float) 0.001;
                    kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                    c[1] = -1f/(8f + (24f*kk));
                } while (((((loadd*z)*panelwidth)/12f) - ((((loadd*z)*panelwidth)*c[1])/6f)) <= (interiorclip/sfic));
                span[5] = z - ((float) 0.001);
                z = 0.1f;
                do
                {
                    z += (float) 0.1;
                    kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                    c[1] = -1f/(8f + (24f*kk));
                } while (((((loadd*z)*panelwidth)/12f) - ((((loadd*z)*panelwidth)*c[1])/6f)) <=
                         ((fastener*nofasic)/sffas));
                do
                {
                    z -= (float) 0.01;
                    kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                    c[1] = -1f/(8f + (24f*kk));
                } while (((((loadd*z)*panelwidth)/12f) - ((((loadd*z)*panelwidth)*c[1])/6f)) >=
                         ((fastener*nofasic)/sffas));
                do
                {
                    z += (float) 0.001;
                    kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                    c[1] = -1f/(8f + (24f*kk));
                } while (((((loadd*z)*panelwidth)/12f) - ((((loadd*z)*panelwidth)*c[1])/6f)) <=
                         ((fastener*nofasic)/sffas));
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

            float allowdouble = span[1];
            String keydouble = Key[1];
            
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
            } while (num2 <= num4);

            z = 0.1f;
            
            do
            {
                z += (float) 0.1;
                kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                c[1] = -1f/(10f + (12f*kk));
            } while ((((((-12f*loadd)*z)*z)*c[1])/sbt) <= (fb/sffb));
            do
            {
                z -= (float) 0.01;
                kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                c[1] = -1f/(10f + (12f*kk));
            } while ((((((-12f*loadd)*z)*z)*c[1])/sbt) >= (fb/sffb));
            do
            {
                z += (float) 0.001;
                kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                c[1] = -1f/(10f + (12f*kk));
            } while ((((((-12f*loadd)*z)*z)*c[1])/sbt) <= (fb/sffb));
            span[1] = z - ((float) 0.001);
            z = 0.1f;
            do
            {
                z += (float) 0.1;
                kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                c[1] = -1f/(10f + (12f*kk));
                zz = ((float) 0.5) + c[1];
            } while (((((((((0.5 + c[1])*zz) - ((0.5*zz)*zz))*loadd)*z)*z)*12.0)/((double) sbb)) <= (fb/sffb));
            do
            {
                z -= (float) 0.01;
                kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                c[1] = -1f/(10f + (12f*kk));
                zz = ((float) 0.5) + c[1];
            } while (((((((((0.5 + c[1])*zz) - ((0.5*zz)*zz))*loadd)*z)*z)*12.0)/((double) sbb)) >= (fb/sffb));
            do
            {
                z += (float) 0.001;
                kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                c[1] = -1f/(10f + (12f*kk));
                zz = ((float) 0.5) + c[1];
            } while (((((((((0.5 + c[1])*zz) - ((0.5*zz)*zz))*loadd)*z)*z)*12.0)/((double) sbb)) <= (fb/sffb));
            span[2] = z - ((float) 0.001);
            span[3] = 10000f;
            span[4] = 10000f;
            span[5] = 10000f;
            span[6] = 10000f;
            z = 0.1f;
            do
            {
                z += (float) 0.1;
                kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                c[1] = -1f/(10f + (12f*kk));
            } while ((((loadd*z)/(2f*ac)) - (((loadd*z)*c[1])/ac)) <= (fv/sffv));
            do
            {
                z -= (float) 0.01;
                kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                c[1] = -1f/(10f + (12f*kk));
            } while ((((loadd*z)/(2f*ac)) - (((loadd*z)*c[1])/ac)) >= (fv/sffv));
            do
            {
                z += (float) 0.001;
                kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                c[1] = -1f/(10f + (12f*kk));
            } while ((((loadd*z)/(2f*ac)) - (((loadd*z)*c[1])/ac)) <= (fv/sffv));
            span[7] = z - ((float) 0.001);
            span[8] = 0f;
            do
            {
                span[8]++;
                kk = (esteel*ic)/((((144f*ac)*gc)*span[8])*span[8]);
                c[1] = -1f/(10f + (12f*kk));
                zz = 0f;
                z = 0f;
                do
                {
                    zzz = z;
                    zz += (float) 0.01;
                    z =
                        (float)
                            ((((1728f*loadd)*Math.Pow((double) span[8], 4.0))/((double) ((24f*esteel)*ic)))*
                             ((((((1f + (12f*kk)) + (4f*c[1]))*zz) - (((12f*kk)*zz)*zz)) -
                               (((4f*c[1]) + 2f)*Math.Pow((double) zz, 3.0))) + Math.Pow((double) zz, 4.0)));
                } while (z >= zzz);
                do
                {
                    zzz = z;
                    zz -= (float) 0.001;
                    z =
                        (float)
                            ((((1728f*loadd)*Math.Pow((double) span[8], 4.0))/((double) ((24f*esteel)*ic)))*
                             ((((((1f + (12f*kk)) + (4f*c[1]))*zz) - (((12f*kk)*zz)*zz)) -
                               (((4f*c[1]) + 2f)*Math.Pow((double) zz, 3.0))) + Math.Pow((double) zz, 4.0)));
                } while (z >= zzz);
                do
                {
                    zzz = z;
                    zz += (float) 0.0001;
                    z =
                        (float)
                            ((((1728f*loadd)*Math.Pow((double) span[8], 4.0))/((double) ((24f*esteel)*ic)))*
                             ((((((1f + (12f*kk)) + (4f*c[1]))*zz) - (((12f*kk)*zz)*zz)) -
                               (((4f*c[1]) + 2f)*Math.Pow((double) zz, 3.0))) + Math.Pow((double) zz, 4.0)));
                } while (z >= zzz);
            } while (z <= ((span[8]*12f)/defrat));
            do
            {
                span[8] -= (float) 0.1;
                kk = (esteel*ic)/((((144f*ac)*gc)*span[8])*span[8]);
                c[1] = -1f/(10f + (12f*kk));
                zz = 0f;
                z = 0f;
                do
                {
                    zzz = z;
                    zz += (float) 0.01;
                    z =
                        (float)
                            ((((1728f*loadd)*Math.Pow((double) span[8], 4.0))/((double) ((24f*esteel)*ic)))*
                             ((((((1f + (12f*kk)) + (4f*c[1]))*zz) - (((12f*kk)*zz)*zz)) -
                               (((4f*c[1]) + 2f)*Math.Pow((double) zz, 3.0))) + Math.Pow((double) zz, 4.0)));
                } while (z >= zzz);
                do
                {
                    zzz = z;
                    zz -= (float) 0.001;
                    z =
                        (float)
                            ((((1728f*loadd)*Math.Pow((double) span[8], 4.0))/((double) ((24f*esteel)*ic)))*
                             ((((((1f + (12f*kk)) + (4f*c[1]))*zz) - (((12f*kk)*zz)*zz)) -
                               (((4f*c[1]) + 2f)*Math.Pow((double) zz, 3.0))) + Math.Pow((double) zz, 4.0)));
                } while (z >= zzz);
                do
                {
                    zzz = z;
                    zz += (float) 0.0001;
                    z =
                        (float)
                            ((((1728f*loadd)*Math.Pow((double) span[8], 4.0))/((double) ((24f*esteel)*ic)))*
                             ((((((1f + (12f*kk)) + (4f*c[1]))*zz) - (((12f*kk)*zz)*zz)) -
                               (((4f*c[1]) + 2f)*Math.Pow((double) zz, 3.0))) + Math.Pow((double) zz, 4.0)));
                } while (z >= zzz);
            } while (z >= ((span[8]*12f)/defrat));
            do
            {
                span[8] += (float) 0.01;
                kk = (esteel*ic)/((((144f*ac)*gc)*span[8])*span[8]);
                c[1] = -1f/(10f + (12f*kk));
                zz = 0f;
                z = 0f;
                do
                {
                    zzz = z;
                    zz += (float) 0.01;
                    z =
                        (float)
                            ((((1728f*loadd)*Math.Pow((double) span[8], 4.0))/((double) ((24f*esteel)*ic)))*
                             ((((((1f + (12f*kk)) + (4f*c[1]))*zz) - (((12f*kk)*zz)*zz)) -
                               (((4f*c[1]) + 2f)*Math.Pow((double) zz, 3.0))) + Math.Pow((double) zz, 4.0)));
                } while (z >= zzz);
                do
                {
                    zzz = z;
                    zz -= (float) 0.001;
                    z =
                        (float)
                            ((((1728f*loadd)*Math.Pow((double) span[8], 4.0))/((double) ((24f*esteel)*ic)))*
                             ((((((1f + (12f*kk)) + (4f*c[1]))*zz) - (((12f*kk)*zz)*zz)) -
                               (((4f*c[1]) + 2f)*Math.Pow((double) zz, 3.0))) + Math.Pow((double) zz, 4.0)));
                } while (z >= zzz);
                do
                {
                    zzz = z;
                    zz += (float) 0.0001;
                    z =
                        (float)
                            ((((1728f*loadd)*Math.Pow((double) span[8], 4.0))/((double) ((24f*esteel)*ic)))*
                             ((((((1f + (12f*kk)) + (4f*c[1]))*zz) - (((12f*kk)*zz)*zz)) -
                               (((4f*c[1]) + 2f)*Math.Pow((double) zz, 3.0))) + Math.Pow((double) zz, 4.0)));
                } while (z >= zzz);
            } while (z <= ((span[8]*12f)/defrat));
            span[8] -= (float) 0.01;

            //if (RadioButton4.Checked)
            //if (App.PanelAttribs.LoadType.EvalType.Equals(LoadTypeEval.Wind))
			if(load)
            {
                z = 0.1f;
                do
                {
                    z += (float) 0.1;
                    kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                    c[1] = -1f/(10f + (12f*kk));
                } while (((((loadd*z)*panelwidth)/24f) + ((((loadd*z)*panelwidth)*c[1])/12f)) <= (endclip/sfec));
                do
                {
                    z -= (float) 0.01;
                    kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                    c[1] = -1f/(10f + (12f*kk));
                } while (((((loadd*z)*panelwidth)/24f) + ((((loadd*z)*panelwidth)*c[1])/12f)) >= (endclip/sfec));
                do
                {
                    z += (float) 0.001;
                    kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                    c[1] = -1f/(10f + (12f*kk));
                } while (((((loadd*z)*panelwidth)/24f) + ((((loadd*z)*panelwidth)*c[1])/12f)) <= (endclip/sfec));
                span[3] = z - ((float) 0.001);
                z = 0.1f;
                do
                {
                    z += (float) 0.1;
                    kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                    c[1] = -1f/(10f + (12f*kk));
                } while (((((loadd*z)*panelwidth)/24f) + ((((loadd*z)*panelwidth)*c[1])/12f)) <=
                         ((fastener*nofasec)/sffas));
                do
                {
                    z -= (float) 0.01;
                    kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                    c[1] = -1f/(10f + (12f*kk));
                } while (((((loadd*z)*panelwidth)/24f) + ((((loadd*z)*panelwidth)*c[1])/12f)) >=
                         ((fastener*nofasec)/sffas));
                do
                {
                    z += (float) 0.001;
                    kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                    c[1] = -1f/(10f + (12f*kk));
                } while (((((loadd*z)*panelwidth)/24f) + ((((loadd*z)*panelwidth)*c[1])/12f)) <=
                         ((fastener*nofasec)/sffas));
                span[4] = z - ((float) 0.001);
                z = 0.1f;
                do
                {
                    z += (float) 0.1;
                    kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                    c[1] = -1f/(10f + (12f*kk));
                } while (((((loadd*z)*panelwidth)/12f) - ((((loadd*z)*panelwidth)*c[1])/12f)) <= (interiorclip/sfic));
                do
                {
                    z -= (float) 0.01;
                    kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                    c[1] = -1f/(10f + (12f*kk));
                } while (((((loadd*z)*panelwidth)/12f) - ((((loadd*z)*panelwidth)*c[1])/12f)) >= (interiorclip/sfic));
                do
                {
                    z += (float) 0.001;
                    kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                    c[1] = -1f/(10f + (12f*kk));
                } while (((((loadd*z)*panelwidth)/12f) - ((((loadd*z)*panelwidth)*c[1])/12f)) <= (interiorclip/sfic));
                span[5] = z - ((float) 0.001);
                z = 0.1f;
                do
                {
                    z += (float) 0.1;
                    kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                    c[1] = -1f/(10f + (12f*kk));
                } while (((((loadd*z)*panelwidth)/12f) - ((((loadd*z)*panelwidth)*c[1])/12f)) <=
                         ((fastener*nofasic)/sffas));
                do
                {
                    z -= (float) 0.01;
                    kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                    c[1] = -1f/(10f + (12f*kk));
                } while (((((loadd*z)*panelwidth)/12f) - ((((loadd*z)*panelwidth)*c[1])/12f)) >=
                         ((fastener*nofasic)/sffas));
                do
                {
                    z += (float) 0.001;
                    kk = (esteel*ic)/((((144f*ac)*gc)*z)*z);
                    c[1] = -1f/(10f + (12f*kk));
                } while (((((loadd*z)*panelwidth)/12f) - ((((loadd*z)*panelwidth)*c[1])/12f)) <=
                         ((fastener*nofasic)/sffas));
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
            
            var allowtriple = span[1];
            var keytriple = Key[1];
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
            } while (num3 <= num4);
			
			retVal.spans = span;
			retVal.spann = spann;
			retVal.allowsingle = allowsingle;
			retVal.allowdouble = allowdouble;
			retVal.allowtriple = allowtriple;
			retVal.keysingle = keysingle;
			retVal.keydouble = keydouble;
			retVal.keytriple = keytriple;
			
			return retVal;
        }