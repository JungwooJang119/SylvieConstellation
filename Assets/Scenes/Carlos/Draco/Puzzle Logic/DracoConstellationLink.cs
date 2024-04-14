public struct DracoConstellationLink {
    public DracoConstellationPoint point1;
    public DracoConstellationPoint point2;

    public DracoConstellationLink(DracoConstellationPoint point1,
                                  DracoConstellationPoint point2) {
        if (point1.NodeID == point2.NodeID) {
            throw new System.InvalidOperationException("Node attempted link to self;");
        }
        if (point1.NodeID > point2.NodeID) {
            this.point1 = point1;
            this.point2 = point2;
        } else {
            this.point2 = point1;
            this.point1 = point2;
        }
    }
}