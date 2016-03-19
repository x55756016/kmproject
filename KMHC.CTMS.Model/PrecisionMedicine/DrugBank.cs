/*
 * 描述:
 *  
 * 修订历史: 
 * 日期       修改人              Email                  内容
 *    		  林德力         	takalin@qq.com   		 创建 
 *  
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.PrecisionMedicine
{

    public class DrugBank
    {

        public DrugBank()
        {
            DrugInteractionInfos = new List<DrugInteractionInfo>();
            Enzymes = new HashSet<Enzymes>();
            Targets = new HashSet<Targets>();
            Transporters = new HashSet<Transporters>();
        }

        public decimal Id { get; set; }
        public string DrugBankId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string DirectParent { get; set; }
        public string SuperClass { get; set; }
        public string Indication { get; set; }
        public string Pharmacodynamics { get; set; }
        public string MechanismOfAction { get; set; }
        public string Toxicity { get; set; }
        public string Metabolism { get; set; }
        public string Absorption { get; set; }
        public string ProteinBinding { get; set; }

        public virtual List<DrugInteractionInfo> DrugInteractionInfos { get; set; }

        public virtual ICollection<Enzymes> Enzymes { get; set; }

        public virtual ICollection<Targets> Targets { get; set; }

        public virtual ICollection<Transporters> Transporters { get; set; }
    }

    public class DrugInteractions
    {
        public decimal Id { get; set; }
        public string DrugBankId { get; set; }
        public string LinkDrugBankId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    #region 代谢酶信息
    public partial class Enzymes
    {
        public Enzymes()
        {
            EnzymePo = new HashSet<EnzymePo>();
            EnzymesActions = new HashSet<EnzymesActions>();
        }
        public decimal Id { get; set; }
        public string DrugBankId { get; set; }
        public string EnzymesId { get; set; }
        public string Name { get; set; }
        public string Organism { get; set; }
        public virtual ICollection<EnzymePo> EnzymePo { get; set; }
        public virtual ICollection<EnzymesActions> EnzymesActions { get; set; }
    }

    public partial class EnzymesActions
    {
        public decimal Id { get; set; }
        public decimal ItemId { get; set; }
        public string EnzymesId { get; set; }
        public string Actions { get; set; }
    }

    public partial class EnzymePo
    {
        public decimal Id { get; set; }
        public decimal ItemId { get; set; }
        public string EnzymeId { get; set; }
        public string PolypeptideId { get; set; }
        public string GeneName { get; set; }
        public string AminoAcidSequence { get; set; }
        public string GeneSequence { get; set; }
    }
    #endregion

    #region 靶点相关信息

    public partial class Targets
    {
        public Targets()
        {
            TargetActions = new HashSet<TargetActions>();
            TargetsPo = new HashSet<TargetsPo>();
        }
        public decimal Id { get; set; }
        public string DrugBankId { get; set; }
        public string TargetId { get; set; }
        public string Name { get; set; }
        public string Organism { get; set; }
        public virtual ICollection<TargetActions> TargetActions { get; set; }
        public virtual ICollection<TargetsPo> TargetsPo { get; set; }
    }


    public partial class TargetActions
    {
        public decimal Id { get; set; }

        public decimal ItemId { get; set; }

        public string TargetId { get; set; }

        public string Action { get; set; }
    }

    public partial class TargetsPo
    {
        public decimal Id { get; set; }
        public decimal ItemId { get; set; }
        public string TargetId { get; set; }
        public string PolypeptideId { get; set; }
        public string GeneName { get; set; }
        public string AminoAcidSequence { get; set; }
        public string GeneSequence { get; set; }
        public virtual Targets Targets { get; set; }
    }

    #endregion

    #region 转运酶相关信息
    public class Transporters
    {
        public Transporters()
        {
            TransporterActions = new HashSet<TransporterActions>();
            TransportersPo = new HashSet<TransportersPo>();
        }

        public decimal Id { get; set; }
        public string DrugBankId { get; set; }

        public string TransportersId { get; set; }

        public string Name { get; set; }

        public string Organism { get; set; }

        public virtual ICollection<TransporterActions> TransporterActions { get; set; }

        public virtual ICollection<TransportersPo> TransportersPo { get; set; }
    }

    public class TransporterActions
    {
        public decimal Id { get; set; }

        public decimal ItemId { get; set; }

        public string TransportersId { get; set; }

        public string Action { get; set; }
    }

    public class TransportersPo
    {
        public decimal Id { get; set; }

        public decimal ItemId { get; set; }

        public string TransportersId { get; set; }

        public string PolypeptideId { get; set; }

        public string GeneName { get; set; }

        public string AminoAcidSequence { get; set; }

        public string GeneSequence { get; set; }
    }
    #endregion




    

    public class DrugInteractionInfo
    {
        public string DrugBankId { get; set; }

        public string InterDrugId { get; set; }

        public string DrugName { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string Description { get; set; }
        public string Toxicity { get; set; }
        public string DrugInter { get; set; }
    }


    public class DrugGeneInfo
    {
        public decimal Id { get; set; }
        public string DrugName { get; set; }

        public string GeneName { get; set; }

        public List<string> Action { get; set; } 

    }



}
