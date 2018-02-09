using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Interview
{
    public class ObsFinalTestResult : Entity<Guid>
    {
        [Indexed]
        public Guid? FirstTestResult { get; set; }
        public string FirstTestResultCode { get; set; }
        [Indexed]
        public Guid? SecondTestResult { get; set; }
        public string SecondTestResultCode { get; set; }
        [Indexed]
        public Guid? FinalResult { get; set; }
        public string FinalResultCode { get; set; }
        [Indexed]
        public Guid? ResultGiven { get; set; }
        [Indexed]
        public Guid? CoupleDiscordant { get; set; }
        [Indexed]
        public Guid? SelfTestOption { get; set; }
        public string Remarks { get; set; }
        [Indexed]
        public Guid EncounterId { get; set; }


        public ObsFinalTestResult()
        {
            Id = LiveGuid.NewGuid();
        }

        private ObsFinalTestResult(Guid id, Guid? firstTestResult, Guid? secondTestResult, Guid? endResult, Guid? resultGiven, Guid? coupleDiscordant, string remarks,
            Guid encounterId)
        {
            Id = id;
            FirstTestResult = firstTestResult;
            SecondTestResult = secondTestResult;
            FinalResult = endResult;
            ResultGiven = resultGiven;
            CoupleDiscordant = coupleDiscordant;
            Remarks = remarks;
            EncounterId = encounterId;
        }

        private ObsFinalTestResult(Guid? firstTestResult, Guid? secondTestResult, Guid? endResult, Guid? resultGiven, Guid? coupleDiscordant, string remarks, Guid encounterId) :
            this(LiveGuid.NewGuid(), firstTestResult, secondTestResult, endResult, resultGiven, coupleDiscordant, remarks,encounterId)
        {

        }

        public static ObsFinalTestResult Create(Guid id, Guid? firstTestResult, Guid? secondTestResult, Guid? endResult,Guid? resultGiven, Guid? coupleDiscordant, string remarks,
            Guid encounterId)
        {
            return new ObsFinalTestResult(id, firstTestResult, secondTestResult, endResult, resultGiven, coupleDiscordant, remarks,encounterId);
        }

        public static ObsFinalTestResult Create(Guid? firstTestResult, Guid? secondTestResult, Guid? endResult, Guid? resultGiven, Guid? coupleDiscordant, string remarks,
            Guid encounterId)
        {
            return new ObsFinalTestResult(firstTestResult, secondTestResult, endResult, resultGiven, coupleDiscordant, remarks, encounterId);
        }

        public static ObsFinalTestResult CreateFirst(Guid? firstTestResult, Guid encounterId)
        {
            return new ObsFinalTestResult(firstTestResult, null, null, null, null, String.Empty,  encounterId);
        }

        public void UpdateSetFirstResult(Guid? result)
        {
            FirstTestResult = result.IsNullOrEmpty() ? null : result;
        }

        public void UpdateSetSecondResult(Guid? result)
        {
            SecondTestResult = result.IsNullOrEmpty() ? null : result;
        }

        public void UpdateSetEndResult(Guid? result)
        {
            FinalResult = result.IsNullOrEmpty() ? null : result;
        }

        public void ProcessEndResult(List<CategoryItem> _categoryItems)
        {
            var neg = _categoryItems.FirstOrDefault(x => x.Item.Code.ToLower() == "N".ToLower());
            var pos = _categoryItems.FirstOrDefault(x => x.Item.Code.ToLower() == "P".ToLower());
            var inv = _categoryItems.FirstOrDefault(x => x.Item.Code.ToLower() == "I".ToLower());
            var ic = _categoryItems.FirstOrDefault(x => x.Item.Code.ToLower() == "IC".ToLower());


            // NO First Result

            if (FirstTestResult.IsNullOrEmpty())
            {
                UpdateSetEndResult(Guid.Empty);
                return;
            }

            // NEG First Result

            if (
                !FirstTestResult.IsNullOrEmpty() && null != neg &&
                FirstTestResult == neg.ItemId)
            {
                UpdateSetSecondResult(Guid.Empty);
                UpdateSetEndResult(neg.ItemId);
                return;
            }


            // Pos | NUll > NULL

            if (
                !FirstTestResult.IsNullOrEmpty() && null != pos && FirstTestResult == pos.ItemId &&
                SecondTestResult.IsNullOrEmpty()
            )
            {
                UpdateSetSecondResult(Guid.Empty);
                UpdateSetEndResult(Guid.Empty);
                return;
            }

            // null | Pos|Neg > NULL

            if (
                FirstTestResult.IsNullOrEmpty() && !SecondTestResult.IsNullOrEmpty()
            )
            {
                UpdateSetEndResult(Guid.Empty);
                return;
            }

            // Pos | Pos > Pos

            if (
                !FirstTestResult.IsNullOrEmpty() && null != pos && FirstTestResult == pos.ItemId &&
                !SecondTestResult.IsNullOrEmpty() && SecondTestResult == pos.ItemId
            )
            {
                UpdateSetEndResult(pos.ItemId);
                return;
            }

            // Pos | Neg > Ic

            if (
                !FirstTestResult.IsNullOrEmpty() && null != pos && FirstTestResult == pos.ItemId &&
                !SecondTestResult.IsNullOrEmpty() && null != neg && SecondTestResult == neg.ItemId
            )
            {
                if (null != ic)
                {
                    UpdateSetEndResult(ic.ItemId);
                }
                return;
            }

        }
    }
}