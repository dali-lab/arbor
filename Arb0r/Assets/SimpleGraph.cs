// A simple graph class and a class to aid with the construction of graphs.
// A Stark for Unity Technologies, 5 Oct 2010.

using System.Collections;

[System.Serializable]
public class SimpleGraph {
//	The arcs array is simply all the arcs in the graph listed in order of the nodes they extend from
//	(so all the arcs from node 0 are listed first, in order, then all the arcs from node 1, etc). The nodes
//	array is then just a list of offsets into the arcs, each element containing the starting point in the
//	arcs array for that particular node. The number of arcs from a node can be calculated by subtracting
//	its offset from the offset of the following node. This necessitates an additional dummy node at the
//	end of the array that simply contains the total number of arcs.
	public int[] nodes;
	public int[] arcs;
	
	
	public SimpleGraph(int numNodes, int numArcs) {
		nodes = new int[numNodes + 1];
		nodes[nodes.Length - 1] = numArcs;
		arcs = new int[numArcs];
	}
	
	
//	Number of nodes in the graph.
	public int numNodes {
		get { return nodes.Length - 1; }
	}

//	Total number of arcs for all nodes.
	public int numArcs {
		get { return arcs.Length; }
	}
	
	
//	Find how many arcs extend from a given node.
	public int NumArcsForNode(int nodeNum) {
		return nodes[nodeNum + 1] - nodes[nodeNum];
	}
	

//	Get an indexed arc from a node.
	public int GetNodeArc(int nodeNum, int arcIndex) {
		return arcs[nodes[nodeNum] + arcIndex];
	}
	

//	Get all a node's arcs in order as an array.
	public int[] GetNodeArcs(int nodeNum) {
		int[] result = new int[NumArcsForNode(nodeNum)];
		System.Array.Copy(arcs, nodes[nodeNum], result, 0, NumArcsForNode(nodeNum));
		return result;
	}
	

//	Set the target node for an arc.
	public void SetNodeArc(int nodeNum, int arcIndex, int newTargetNode) {
		arcs[nodes[nodeNum] + arcIndex] = newTargetNode;
	}
	
}



// This simplifies the construction of a SimpleGraph by allowing the nodes and arcs to be added
// incrementally without prior knowledge of how many of each there will be.
// The basic plan is to start a new node, then add as many arcs as necessary, then start another
// new node, etc. When the temporary graph is complete, the Build function will construct
// the equivalent SimpleGraph.

public class SimpleGraphBuilder {
	ArrayList nodes;
	int arcTotal;
	
	public SimpleGraphBuilder() {
		nodes = new ArrayList();
	}
	

//	Start a new node.
	public void NewNode() {
		nodes.Add(new ArrayList());
	}
	

//	Add arcs to the current node.
	public void AddArcs(params int[] targetNodes) {
		ArrayList currNode = (ArrayList) nodes[nodes.Count - 1];
		
		for (int i = 0; i < targetNodes.Length; i++) {
			currNode.Add(targetNodes[i]);
		}
		
		arcTotal += targetNodes.Length;
	}
	
	
//	Build a SimpleGraph from the temporary graph contained by the builder.
	public SimpleGraph Build() {
		SimpleGraph result = new SimpleGraph(nodes.Count, arcTotal);
		int currArc = 0;
		
		for (int i = 0; i < nodes.Count; i++) {
			result.nodes[i] = currArc;
			ArrayList currNode = (ArrayList) nodes[i];
			currNode.CopyTo(result.arcs, currArc);
			currArc += currNode.Count;
		}
		
		return result;
	}
}