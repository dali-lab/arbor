using UnityEngine;
using System.Collections;

public class Parser : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	/**
	This method parses a given .dta file and returns a graph object detailing the phylogeny tree. 
	
	The method parses the file to input the number taxa, the number of 
    characters, the phylogenetic tree in Newick format, and the character data for 
    each taxon in the tree. 
	
	<p>The format of the dataset must be as follows, for example:</p> 
    <p>4 2<p/>
    <p>((A:2.4,B:2.4):2.4,(C:3.4,D:3.4):1.4)</p>
    <p>A    2.3    2.4</p>
    <p>B    1.4    10.4</p>
    <p>C    5.3    4.8</p>
    <p>D    9.8    2.2</p>
    <p>variables</p>
    <P>Hind Wing Length</P>
    <P>Abdomen Width</P>
    <P> </P>
	**/
	
	public void GraphfromFile( string filename ) {
		
		FileInfo source = null;
		Streamreader reader = null; 
		string linebuffer; 
		
		var builder = new SimpleGraphBuilder();
		
		source = new FileInfo(Application.dataPath + filename);
		
		if (source != null && source.Exists )
			reader = source.OpenText();
		
		linebuffer = reader.ReadLine();
		
	   	
	
		// building the graph 
	    while (line != null) {
	
	        builder.NewNode();
	
	        var arcs = line.Split();
	
	        
	
	        for (int i = 0; i < arcs.Length; i++) {
	
	            var arcNum = System.Int32.Parse(arcs[i]);
	
	            builder.AddArcs(arcNum);
	
	        }
	
	        
	
	        line = reader.ReadLine();
	
	    }
	
	    
	
	    return builder.Build();

	}
}

