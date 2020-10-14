<Query Kind="Program" />

void Main()
{
	double linerthickness = double.MinValue;
	double panelthickness = double.MinValue;
	double fb = double.MinValue;
	double reveal = double.MinValue;
	


	if (linerthickness > 0.0298)
                {
                    if (panelThickness == 2.0)
                    {
                        fb = 20992f;
                    }
                    else if (panelThickness == 2.5)
                    {
                        fb = 25980f;
                    }
                    else
                    {
                        fb = 18360f;
                    }
                }

                else if (panelThickness == 2.0)
                {
                    fb = 24438f;
                }
                else if (panelThickness == 2.5)
                {
                    fb = 28700f;
                }
                else
                {
                    fb = 17211f;
                }
}

// Define other methods and classes here
