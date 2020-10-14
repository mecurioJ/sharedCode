<Query Kind="Program" />

void Main()
{
double.MinValue.Dump();

	var TruthTable = new []{
		new TruthItem 
		{
			Model = PanelModel.FWDS,
			PanelThickness = 2.0,
			UseLinerThreshold = true,
			LinerThicknessThreshold = 0.0298,
			RibToClipFastener = false,
			ThicknessPlusRib = 0.0,
			ShearModulusGC = 733.0,
			BendingStressFB = 20992.0,
			ShearStressFV = 36.26,
			REndEndClip = 1077.0,
			RIntInteriorClip = 2157.0,
			Reveal = 0.5
		},
		new TruthItem 
		{
			Model = PanelModel.FWDS,
			PanelThickness = 2.5,
			UseLinerThreshold = true,
			LinerThicknessThreshold = 0.0298,
			RibToClipFastener = false,
			ThicknessPlusRib = 0.0,
			ShearModulusGC = 907.0,
			BendingStressFB = 25980.0,
			ShearStressFV = 23.8,
			REndEndClip = 680.0,
			RIntInteriorClip = 1686.0,
			Reveal = 0.5
		},
		new TruthItem 
		{
			Model = PanelModel.FWDS,
			PanelThickness = 3.0,
			UseLinerThreshold = true,
			LinerThicknessThreshold = 0.0298,
			RibToClipFastener = false,
			ThicknessPlusRib = 0.0,
			ShearModulusGC = 730.0,
			BendingStressFB = 20030.0,
			ShearStressFV = 26.5,
			REndEndClip = 734.0,
			RIntInteriorClip = 2116.0,
			Reveal = 0.5
		},
		
		new TruthItem 
		{
			Model = PanelModel.FWDS,
			PanelThickness = 2.0,
			UseLinerThreshold = false,
			LinerThicknessThreshold = 0.0298,
			RibToClipFastener = false,
			ThicknessPlusRib = 0.0,
			ShearModulusGC = 813.0,
			BendingStressFB = 24438.0,
			ShearStressFV = 36.26,
			REndEndClip = 804.0,
			RIntInteriorClip = 2084.0,
			Reveal = 0.5
		},
		new TruthItem 
		{
			Model = PanelModel.FWDS,
			PanelThickness = 2.5,
			UseLinerThreshold = false,
			LinerThicknessThreshold = 0.0298,
			RibToClipFastener = false,
			ThicknessPlusRib = 0.0,
			ShearModulusGC = 907.0,
			BendingStressFB = 28700.0,
			ShearStressFV = 31.0,
			REndEndClip = 649.0,
			RIntInteriorClip = 1620.0,
			Reveal = 0.5
		},
		new TruthItem 
		{
			Model = PanelModel.FWDS3T,
			PanelThickness = 3.0,
			UseLinerThreshold = false,
			LinerThicknessThreshold = 0.0298,
			RibToClipFastener = false,
			ThicknessPlusRib = 0.0,
			ShearModulusGC = 730.0,
			BendingStressFB = 20030.0,
			ShearStressFV = 26.5,
			REndEndClip = 734.0,
			RIntInteriorClip = 2116.0,
			Reveal = 0.5
		},
		
		
		new TruthItem 
		{
			Model = PanelModel.FWDS59,
			PanelThickness = 2.0,
			UseLinerThreshold = false,
			LinerThicknessThreshold = 0.0298,
			RibToClipFastener = false,
			ThicknessPlusRib = 0.0,
			ShearModulusGC = 840.0,
			BendingStressFB = 23751.0,
			ShearStressFV = 36.26,
			REndEndClip = 804.0,
			RIntInteriorClip = 2084.0,
			Reveal = 0.0
		},
		new TruthItem 
		{
			Model = PanelModel.FWDS59,
			PanelThickness = 2.5,
			UseLinerThreshold = false,
			LinerThicknessThreshold = 0.0298,
			RibToClipFastener = false,
			ThicknessPlusRib = 0.0,
			ShearModulusGC = 2670.0,
			BendingStressFB = 28550.0,
			ShearStressFV = 68.3,
			REndEndClip = 698.0,
			RIntInteriorClip = 1600.0,
			Reveal = 0.0
		},
		
		new TruthItem 
		{
			Model = PanelModel.FWDS60,
			PanelThickness = 2.0,
			UseLinerThreshold = false,
			LinerThicknessThreshold = 0.0298,
			RibToClipFastener = false,
			ThicknessPlusRib = 0.0,
			ShearModulusGC = 796.0,
			BendingStressFB = 22999.0,
			ShearStressFV = 36.26,
			REndEndClip = 804.0,
			RIntInteriorClip = 2084.0,
			Reveal = 0.0
		},
		new TruthItem 
		{
			Model = PanelModel.FWDS60,
			PanelThickness = 2.5,
			UseLinerThreshold = false,
			LinerThicknessThreshold = 0.0298,
			RibToClipFastener = false,
			ThicknessPlusRib = 0.0,
			ShearModulusGC = 2670.0,
			BendingStressFB = 28550.0,
			ShearStressFV = 68.3,
			REndEndClip = 698.0,
			RIntInteriorClip = 1600.0,
			Reveal = 0.0
		},
		
		new TruthItem 
		{
			Model = PanelModel.DS593T,
			PanelThickness = 3.0,
			UseLinerThreshold = false,
			LinerThicknessThreshold = 0.0298,
			RibToClipFastener = false,
			ThicknessPlusRib = 0.0,
			ShearModulusGC = 2670.0,
			BendingStressFB = 28550.0,
			ShearStressFV = 68.3,
			REndEndClip = 698.0,
			RIntInteriorClip = 1600.0,
			Reveal = 0.0
		},
		new TruthItem 
		{
			Model = PanelModel.DS603T,
			PanelThickness = 3.0,
			UseLinerThreshold = false,
			LinerThicknessThreshold = 0.0298,
			RibToClipFastener = false,
			ThicknessPlusRib = 0.0,
			ShearModulusGC = 2670.0,
			BendingStressFB = 28550.0,
			ShearStressFV = 68.3,
			REndEndClip = 698.0,
			RIntInteriorClip = 1600.0,
			Reveal = 0.0
		},
		new TruthItem 
		{
			Model = PanelModel.VW,
			PanelThickness = 4.0,
			UseLinerThreshold = true,
			LinerThicknessThreshold = 0.0298,
			RibToClipFastener = false,
			ThicknessPlusRib = 0.0,
			ShearModulusGC = 731.0,
			BendingStressFB = 24009.0,
			ShearStressFV = 17.7,
			REndEndClip = 1028.0,
			RIntInteriorClip = 3337.0,
			Reveal = 0.0
		},		
		new TruthItem 
		{
			Model = PanelModel.VW,
			PanelThickness = 4.0,
			UseLinerThreshold = false,
			LinerThicknessThreshold = 0.0298,
			RibToClipFastener = false,
			ThicknessPlusRib = 0.0,
			ShearModulusGC = 731.0,
			BendingStressFB = 24009.0,
			ShearStressFV = 17.7,
			REndEndClip = 683.0,
			RIntInteriorClip = 3044.0,
			Reveal = 0.0
		},
		new TruthItem 
		{
			Model = PanelModel.VW,
			PanelThickness = 2.75,
			UseLinerThreshold = true,
			LinerThicknessThreshold = 0.0298,
			RibToClipFastener = false,
			ThicknessPlusRib = 0.0,
			ShearModulusGC = 731.0,
			BendingStressFB = 24009.0,
			ShearStressFV = 17.7,
			REndEndClip = 864.0,
			RIntInteriorClip = 2540.0,
			Reveal = 0.0
		},		
		new TruthItem 
		{
			Model = PanelModel.VW,
			PanelThickness = 2.75,
			UseLinerThreshold = false,
			LinerThicknessThreshold = 0.0298,
			RibToClipFastener = false,
			ThicknessPlusRib = 0.0,
			ShearModulusGC = 731.0,
			BendingStressFB = 24009.0,
			ShearStressFV = 17.7,
			REndEndClip = 671.0,
			RIntInteriorClip = 2292.0,
			Reveal = 0.0
		},		
		new TruthItem 
		{
			Model = PanelModel.VW,
			PanelThickness = 2.0,
			UseLinerThreshold = true,
			LinerThicknessThreshold = 0.0298,
			RibToClipFastener = false,
			ThicknessPlusRib = 0.0,
			ShearModulusGC = 731.0,
			BendingStressFB = 24009.0,
			ShearStressFV = 17.7,
			REndEndClip = 864.0,
			RIntInteriorClip = 2540.0,
			Reveal = 0.0
		},		
		new TruthItem 
		{
			Model = PanelModel.VW,
			PanelThickness = 2.0,
			UseLinerThreshold = false,
			LinerThicknessThreshold = 0.0298,
			RibToClipFastener = false,
			ThicknessPlusRib = 0.0,
			ShearModulusGC = 731.0,
			BendingStressFB = 24009.0,
			ShearStressFV = 17.7,
			REndEndClip = 671.0,
			RIntInteriorClip = 2292.0,
			Reveal = 0.0
		},		
		
		new TruthItem 
		{
			Model = PanelModel.VP,
			PanelThickness = 1.75,
			UseLinerThreshold = true,
			LinerThicknessThreshold = 0.0298,
			RibToClipFastener = true,
			ThicknessPlusRib = 3.53125,
			ShearModulusGC = 694.0,
			BendingStressFB = 48269.0,
			ShearStressFV = 26.7,
			REndEndClip = 1688.0,
			RIntInteriorClip = 4376.0,
			Reveal = 0.0
		},
		new TruthItem 
		{
			Model = PanelModel.VP,
			PanelThickness = 1.75,
			UseLinerThreshold = true,
			LinerThicknessThreshold = 0.0298,
			RibToClipFastener = false,
			ThicknessPlusRib = 3.53125,
			ShearModulusGC = 694.0,
			BendingStressFB = 48269.0,
			ShearStressFV = 26.7,
			REndEndClip = 1006.0,
			RIntInteriorClip = 2297.0,
			Reveal = 0.0
		},
		new TruthItem 
		{
			Model = PanelModel.VP,
			PanelThickness = 1.75,
			UseLinerThreshold = false,
			LinerThicknessThreshold = 0.0298,
			RibToClipFastener = true,
			ThicknessPlusRib = 3.53125,
			ShearModulusGC = 694.0,
			BendingStressFB = 48269.0,
			ShearStressFV = 26.7,
			REndEndClip = 1329.0,
			RIntInteriorClip = 3252.0,
			Reveal = 0.0
		},
		new TruthItem 
		{
			Model = PanelModel.VP,
			PanelThickness = 1.75,
			UseLinerThreshold = false,
			LinerThicknessThreshold = 0.0298,
			RibToClipFastener = false,
			ThicknessPlusRib = 3.53125,
			ShearModulusGC = 694.0,
			BendingStressFB = 48269.0,
			ShearStressFV = 26.7,
			REndEndClip = 803.0,
			RIntInteriorClip = 2044.0,
			Reveal = 0.0
		},
		new TruthItem 
		{
			Model = PanelModel.VP,
			PanelThickness = 2.5,
			UseLinerThreshold = true,
			LinerThicknessThreshold = 0.0298,
			RibToClipFastener = true,
			ThicknessPlusRib = 4.25,
			ShearModulusGC = 694.0,
			BendingStressFB = 45040.0,
			ShearStressFV = 24.0,
			REndEndClip = 1980.0,
			RIntInteriorClip = 4921.0,
			Reveal = 0.0
		},
		new TruthItem 
		{
			Model = PanelModel.VP,
			PanelThickness = 2.5,
			UseLinerThreshold = true,
			LinerThicknessThreshold = 0.0298,
			RibToClipFastener = false,
			ThicknessPlusRib = 4.25,
			ShearModulusGC = 694.0,
			BendingStressFB = 45040.0,
			ShearStressFV = 24.0,
			REndEndClip = 1038.0,
			RIntInteriorClip = 2752.0,
			Reveal = 0.0
		},
		new TruthItem 
		{
			Model = PanelModel.VP,
			PanelThickness = 2.5,
			UseLinerThreshold = false,
			LinerThicknessThreshold = 0.0298,
			RibToClipFastener = true,
			ThicknessPlusRib = 4.25,
			ShearModulusGC = 694.0,
			BendingStressFB = 45040.0,
			ShearStressFV = 24.0,
			REndEndClip = 1374.0,
			RIntInteriorClip = 3638.0,
			Reveal = 0.0
		},
		new TruthItem 
		{
			Model = PanelModel.VP,
			PanelThickness = 2.5,
			UseLinerThreshold = false,
			LinerThicknessThreshold = 0.0298,
			RibToClipFastener = false,
			ThicknessPlusRib = 4.25,
			ShearModulusGC = 694.0,
			BendingStressFB = 45040.0,
			ShearStressFV = 24.0,
			REndEndClip = 845.0,
			RIntInteriorClip = 2293.0,
			Reveal = 0.0
		},
		new TruthItem 
		{
			Model = PanelModel.VP,
			PanelThickness = 4.0,
			UseLinerThreshold = true,
			LinerThicknessThreshold = 0.0298,
			RibToClipFastener = true,
			ThicknessPlusRib = 5.75,
			ShearModulusGC = 674.0,
			BendingStressFB = 25192.0,
			ShearStressFV = 19.3,
			REndEndClip = 2109.0,
			RIntInteriorClip = 5502.0,
			Reveal = 0.0
		},
		new TruthItem 
		{
			Model = PanelModel.VP,
			PanelThickness = 4.0,
			UseLinerThreshold = true,
			LinerThicknessThreshold = 0.0298,
			RibToClipFastener = false,
			ThicknessPlusRib = 5.75,
			ShearModulusGC = 674.0,
			BendingStressFB = 25192.0,
			ShearStressFV = 19.3,
			REndEndClip = 1373.0,
			RIntInteriorClip = 4335.0,
			Reveal = 0.0
		},
		new TruthItem 
		{
			Model = PanelModel.VP,
			PanelThickness = 4.0,
			UseLinerThreshold = false,
			LinerThicknessThreshold = 0.0298,
			RibToClipFastener = true,
			ThicknessPlusRib = 5.75,
			ShearModulusGC = 674.0,
			BendingStressFB = 25192.0,
			ShearStressFV = 19.3,
			REndEndClip = 1501.0,
			RIntInteriorClip = 4002.0,
			Reveal = 0.0
		},
		new TruthItem 
		{
			Model = PanelModel.VP,
			PanelThickness = 4.0,
			UseLinerThreshold = false,
			LinerThicknessThreshold = 0.0298,
			RibToClipFastener = false,
			ThicknessPlusRib = 5.75,
			ShearModulusGC = 674.0,
			BendingStressFB = 25192.0,
			ShearStressFV = 19.3,
			REndEndClip = 943.0,
			RIntInteriorClip = 4198.0,
			Reveal = 0.0
		},
		new TruthItem 
		{
			Model = PanelModel.FoamWall,
			PanelThickness = 2.0,
			UseLinerThreshold = false,
			LinerThicknessThreshold = 0.0,
			RibToClipFastener = false,
			ThicknessPlusRib = 0.0,
			ShearModulusGC = 921.0,
			BendingStressFB = 28600.0,
			ShearStressFV = 24.2,
			REndEndClip = 660.0,
			RIntInteriorClip = 1814.0,
			Reveal = 0.5
		},
		new TruthItem 
		{
			Model = PanelModel.MetalwrapH,
			PanelThickness = 2.0,
			UseLinerThreshold = false,
			LinerThicknessThreshold = 0.0,
			RibToClipFastener = false,
			ThicknessPlusRib = 0.0,
			ShearModulusGC = 895.0,
			BendingStressFB = 27000.0,
			ShearStressFV = 28.9,
			REndEndClip = 290.0,
			RIntInteriorClip = 948.0,
			Reveal = 0.0
		},
		new TruthItem 
		{
			Model = PanelModel.MetalWrapV,
			PanelThickness = 4.0,
			UseLinerThreshold = false,
			LinerThicknessThreshold = 0.0,
			RibToClipFastener = false,
			ThicknessPlusRib = 0.0,
			ShearModulusGC = 731.0,
			BendingStressFB = 24009.0,
			ShearStressFV = 17.7,
			REndEndClip = 683.0,
			RIntInteriorClip = 3044.0,
			Reveal = 0.0
		},
		new TruthItem 
		{
			Model = PanelModel.MetalWrapV,
			PanelThickness = 2.75,
			UseLinerThreshold = false,
			LinerThicknessThreshold = 0.0,
			RibToClipFastener = false,
			ThicknessPlusRib = 0.0,
			ShearModulusGC = 802.0,
			BendingStressFB = 25460.0,
			ShearStressFV = 26.2,
			REndEndClip = 671.0,
			RIntInteriorClip = 2292.0,
			Reveal = 0.0
		}
	
	};
	
	TruthTable.Dump();
}

// Define other methods and classes here
public class TruthItem
{
	public PanelModel Model {get;set;}
	public double PanelThickness {get;set;}
	public bool UseLinerThreshold {get;set;}
	public double LinerThicknessThreshold {get;set;}
	public bool RibToClipFastener {get;set;}
	public double ThicknessPlusRib {get;set;}
	public double ShearModulusGC {get;set;}
	public double BendingStressFB {get;set;}
	public double ShearStressFV {get;set;}
	public double REndEndClip {get;set;}
	public double RIntInteriorClip {get;set;}
	public double Reveal {get;set;}
}

public enum PanelModel
{
	FWDSVertical,
	FWDS,
	FWDS3T,
	VW,
	VP,
	FoamWall,
	FWDS59,
	FWDS60,
	DS593T,
	DS603T,
	MetalwrapH,
	MetalWrapV
}