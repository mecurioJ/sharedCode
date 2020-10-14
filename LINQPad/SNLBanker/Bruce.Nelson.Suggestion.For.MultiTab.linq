<Query Kind="Statements" />

            var colAreas = report.ColumnAreas.Select(area => area.UseColumnMeasuresFilter);
            if (!colAreas.Any())
            {
                //do it without tabs.    
            }
            else
            {
                // do it with tabs
 
                foreach (var area in colAreas)
                {
                    var measures = area.Variables["[Measures]"];
                    var measureTab = new
                            {
                                xtype = "panel",
                                title = "_column_area_measures_".Localize(),
                                autoScroll = false,
                                html = CubeMemberList.WriteVariableOptions(measures, areaIndex)
                            };
 
                }
            }
 
            var tabPanel = new
                            {
                                xtype = "tabpanel",
                                enableTabScroll = false,
                                frame = false,
                                border = false,
                                plain = true,
                                deferredRender = false,
                                autoHeight = false,
                                autoScroll = false,
                                bodyStyle = "font-size:10pt",
                                defaults = new { autoScroll = true },
                                items = GetTabs(colAreas)
                            };
 
            var tabs = new[]
                        {
                            new
                            {
                                xtype = "panel",
                                title = "_columns".Localize(),
                                autoScroll = false,
                                html = GetColumnFilterOptions(report, map, mapId)
                            },
                            new
                            {
                                xtype = "panel",
                                title = "_column_area_measures_".Localize(),
                                autoScroll = false,
                                html = CubeMemberList.WriteMembersList(memberList)
                            }
                        };